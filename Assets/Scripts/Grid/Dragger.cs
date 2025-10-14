using UnityEngine;

public class Dragger : MonoBehaviour
{
    [Header("Scene Refs")] [SerializeField]
    private Camera _mainCamera;

    [SerializeField] private GridHandler _gridHandler;
    [SerializeField] private Transform _gridPivot;

    [Header("Input")] [SerializeField] private DesktopInput _desktopInput;

    [Header("Drag Settings")] [SerializeField, Min(0f)]
    private float _smoothTime = 0.08f;

    [SerializeField, Min(0f)] private float _maxSpeed = 100f;

    // Слой для выбираемых объектов (Layer 3 в вашем примере)
    [SerializeField] private int _draggableLayer = 3;

    private GameObject _selectedObject;
    private Vector3 _dragOffset;
    private Vector3 _vel;
    private Vector3 _targetWorldPos;
    private Vector2Int _lastValidCell;
    private Vector2Int _originalCell;
    private int _draggableLayerMask;

    private void Awake()
    {
        _draggableLayerMask = 1 << _draggableLayer;
        if (_gridPivot == null && _gridHandler != null)
            _gridPivot = _gridHandler.Pivot;
    }

    private void Update()
    {
        if (_desktopInput != null)
        {
            if (_desktopInput.IsPointerDown)
                HandleMouseClick();
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
                HandleMouseClick();
        }

        if (_selectedObject != null)
            UpdateSelectedObjectPosition(dragging: true);
    }

    private void HandleMouseClick()
    {
        if (_selectedObject == null)
            TrySelectObject();
        else
            DropObject();
    }

    private void TrySelectObject()
    {
        if (_mainCamera == null || _gridHandler == null || _gridPivot == null)
            return;

        Ray ray = _mainCamera.ScreenPointToRay(GetPointerPosition());
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, _draggableLayerMask))
        {
            _selectedObject = hit.collider.gameObject;

            if (TryGetGridPlaneHit(ray, out Vector3 planeHit))
            {
                _dragOffset = _selectedObject.transform.position - planeHit;
            }
            else
            {
                _dragOffset = Vector3.zero;
            }

            _originalCell = _gridHandler.WorldToCell(_selectedObject.transform.position);
            _gridHandler.ReleaseCell(_originalCell, _selectedObject);

            _lastValidCell = _originalCell;
            _targetWorldPos = _gridHandler.CellToWorld(_lastValidCell);
            _vel = Vector3.zero;
        }
    }

    private void UpdateSelectedObjectPosition(bool dragging)
    {
        if (_selectedObject == null || _mainCamera == null || _gridPivot == null || _gridHandler == null)
            return;

        Ray ray = _mainCamera.ScreenPointToRay(GetPointerPosition());
        if (!TryGetGridPlaneHit(ray, out Vector3 planeHit))
            return;

        Vector3 desiredWorld = planeHit + _dragOffset;

        Vector2Int desiredCell = _gridHandler.WorldToCell(desiredWorld);

        if (!_gridHandler.IsCellOccupied(desiredCell))
        {
            _lastValidCell = desiredCell;
            _targetWorldPos = _gridHandler.CellToWorld(_lastValidCell);
        }

        _selectedObject.transform.position = Vector3.SmoothDamp(
            _selectedObject.transform.position,
            _targetWorldPos,
            ref _vel,
            _smoothTime,
            _maxSpeed
        );
    }

    private void DropObject()
    {
        if (_selectedObject == null || _gridHandler == null)
            return;

        if (!_gridHandler.IsCellOccupied(_lastValidCell))
        {
            _gridHandler.ReserveCell(_lastValidCell, _selectedObject);
            _selectedObject.transform.position = _gridHandler.CellToWorld(_lastValidCell);
        }
        else
        {
            _gridHandler.ReserveCell(_originalCell, _selectedObject);
            _selectedObject.transform.position = _gridHandler.CellToWorld(_originalCell);
        }

        _selectedObject = null;
        _vel = Vector3.zero;
    }

    private bool TryGetGridPlaneHit(Ray ray, out Vector3 hitPoint)
    {
        Plane plane = new Plane(_gridPivot.up, _gridPivot.position);
        if (plane.Raycast(ray, out float enter))
        {
            hitPoint = ray.GetPoint(enter);
            return true;
        }

        hitPoint = default;
        return false;
    }

    private Vector3 GetPointerPosition()
    {
        if (_desktopInput != null)
            return _desktopInput.PointerPosition;
        else
            return Input.mousePosition;
    }
}