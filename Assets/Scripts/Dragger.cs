using UnityEngine;

public class Dragger : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private GridHandler _gridHandler;
    [SerializeField] private InputReader _inputReader;

    private GameObject _selectedObject;
    private int _draggableLayerMask = 1 << 3;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            HandleMouseClick();
        }

        if (_selectedObject != null)
        {
            UpdateSelectedObjectPosition(dragging: true);
        }
    }

    private void HandleMouseClick()
    {
        if (_selectedObject == null)
        {
            TrySelectObject();
        }
        else
        {
            DropObject();
        }
    }

    private void TrySelectObject()
    {
        RaycastHit hit = CastRay();

        if (hit.collider != null && IsOnDragLayer(hit.collider.gameObject))
        {
            _selectedObject = hit.collider.gameObject;
          
            // Cursor.visible = false;
        }
    }

    private void DropObject()
    {
        UpdateSelectedObjectPosition(dragging: false);
        _selectedObject = null;
        Cursor.visible = true;
    }

    private void UpdateSelectedObjectPosition(bool dragging)
    {
        if (_gridHandler.TryGetPosition(out Vector2 position))
        {
            _selectedObject.transform.position = new Vector3(position.x + _gridHandler.CellSize.x / 2, 0,
                position.y + _gridHandler.CellSize.y / 2);
        }
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 screenPosition = new Vector3(
            _inputReader.MouseScreenPosition.x,
            _inputReader.MouseScreenPosition.y,
            _mainCamera.WorldToScreenPoint(_selectedObject.transform.position).z);

        return _mainCamera.ScreenToWorldPoint(screenPosition);
    }

    private RaycastHit CastRay()
    {
        Camera camera = _mainCamera;

        Vector3 screenMousePositionFar = new Vector3(
            _inputReader.MouseScreenPosition.x,
            _inputReader.MouseScreenPosition.y,
            camera.farClipPlane);
        Vector3 screenMousePositionNear = new Vector3(
            _inputReader.MouseScreenPosition.x,
            _inputReader.MouseScreenPosition.y,
            camera.nearClipPlane);

        Vector3 worldMousePositionFar = camera.ScreenToWorldPoint(screenMousePositionFar);
        Vector3 worldMousePositionNear = camera.ScreenToWorldPoint(screenMousePositionNear);

        RaycastHit hit;
        Physics.Raycast(worldMousePositionNear,
            worldMousePositionFar - worldMousePositionNear,
            out hit,
            Mathf.Infinity,
            _draggableLayerMask);

        return hit;
    }

    private bool IsOnDragLayer(GameObject gameObject)
    {
        return (_draggableLayerMask & (1 << gameObject.layer)) != 0;
    }
}