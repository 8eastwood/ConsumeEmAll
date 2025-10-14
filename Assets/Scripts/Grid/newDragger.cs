using UnityEngine;

public class newDragger : MonoBehaviour
{
    [SerializeField] newGridHandler _gridHandler;
    [SerializeField] LayerMask pieceMask = ~0;
    [SerializeField] float rayMaxDistance = 500f;
    [SerializeField] Camera _mainCamera;

    [Header("Rotate Controls")] 
    [SerializeField] KeyCode rotateCCW = KeyCode.Q; // -90°
    [SerializeField] KeyCode rotateCW = KeyCode.E; // +90°

    Piece _dragging;
    Vector3Int _startPivotCell;
    Vector3Int _currentPreviewCell;
    Quaternion _startRotation;

    void Awake()
    {
        if (_mainCamera == null)
            _mainCamera = Camera.main;
    }

    void Update()
    {
        HandleMouse();
        HandleRotate();
        UpdateDrag();
    }

    void HandleMouse()
    {
        if (Input.GetMouseButtonDown(0))
            TryBeginDrag();

        if (Input.GetMouseButtonUp(0))
            EndDrag();
    }

    void HandleRotate()
    {
        if (_dragging == null) return;

        int delta = 0;
        
        if (Input.GetKeyDown(rotateCCW)) 
            delta -= 1;
        
        if (Input.GetKeyDown(rotateCW))
            delta += 1;

        if (delta != 0)
        {
            _dragging.RotateYSteps(delta);
            // После поворота обновим предпросмотр на текущей клетке
            SnapDraggingToCell(_currentPreviewCell, onlyIfValid: false);
        }
    }

    void UpdateDrag()
    {
        if (_dragging == null || _mainCamera == null) return;

        if (!TryProjectMouseToGrid(out var worldOnPlane))
            return;

        var cell = _gridHandler.WorldToCellRound(worldOnPlane - _dragging.pivotWorldOffset);

        if (_gridHandler.lockYToFixedLayer)
            cell.y = Mathf.Clamp(_gridHandler.fixedYLayer, 0, _gridHandler.size.y - 1);

        _currentPreviewCell = cell;
        SnapDraggingToCell(cell, onlyIfValid: true);
    }

    void TryBeginDrag()
    {
        if (_mainCamera == null) return;

        var ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out var hit, rayMaxDistance, pieceMask, QueryTriggerInteraction.Collide))
        {
            var piece = hit.collider.GetComponentInParent<Piece>();
            if (piece == null) return;

            _dragging = piece;
            _startRotation = piece.transform.rotation;
            _startPivotCell = piece.currentPivotCell ??
                              _gridHandler.WorldToCellRound(piece.transform.position - piece.pivotWorldOffset);

            // Подготовка к перетаскиванию: освободим клетки
            _gridHandler.Release(piece);
        }
    }

    void EndDrag()
    {
        if (_dragging == null) return;

        // Если текущая позиция валидна — фиксируем, иначе возвращаем
        var targetCell = _currentPreviewCell;
        if (!_gridHandler.IsPlacementValid(_dragging, targetCell))
        {
            targetCell = _startPivotCell;
            _dragging.transform.rotation = _startRotation; // вернуть исходный визуальный поворот
            _dragging.ApplyVisualRotation();
        }

        _gridHandler.Occupy(_dragging, targetCell, snapTransform: true);
        _dragging = null;
    }

    void SnapDraggingToCell(Vector3Int cell, bool onlyIfValid)
    {
        if (_dragging == null) return;

        bool ok = _gridHandler.IsPlacementValid(_dragging, cell);
        if (onlyIfValid && !ok) return;

        var world = _gridHandler.CellToWorld(cell) + _dragging.pivotWorldOffset;
        _dragging.transform.position = world;
        // Визуальный поворот уже применён в Piece
    }

    bool TryProjectMouseToGrid(out Vector3 worldOnPlane)
    {
        worldOnPlane = default;
        if (_mainCamera == null) return false;

        var ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
        // Плоскость перетаскивания: по умолчанию горизонтальная на фиксированном y
        float y = _gridHandler.lockYToFixedLayer
            ? _gridHandler.GetWorldYForLayer(_gridHandler.fixedYLayer)
            : (_gridHandler.origin.y + (_dragging?.currentPivotCell?.y ?? 0) * _gridHandler.cellSize.y);
        var plane = new Plane(Vector3.up, new Vector3(0f, y, 0f));

        if (plane.Raycast(ray, out float enter))
        {
            worldOnPlane = ray.GetPoint(enter);
            return true;
        }

        return false;
    }
}