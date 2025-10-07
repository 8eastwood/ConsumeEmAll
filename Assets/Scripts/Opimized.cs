using UnityEngine;

public class OptimizedDragDrop : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera;

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
            Cursor.visible = false;
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
        Vector3 position = GetMouseWorldPosition();
        float positionY;

        if (dragging)
            positionY = .25f;
        else
            positionY = 0;

        _selectedObject.transform.position = new Vector3(position.x, positionY, position.z);
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 screenPosition = new Vector3(
            Input.mousePosition.x,
            Input.mousePosition.y,
            _mainCamera.WorldToScreenPoint(_selectedObject.transform.position).z);

        return _mainCamera.ScreenToWorldPoint(screenPosition);
    }

    private RaycastHit CastRay()
    {
        Camera camera = _mainCamera;

        Vector3 screenMousePositionFar = new Vector3(
            Input.mousePosition.x,
            Input.mousePosition.y,
            camera.farClipPlane);
        Vector3 screenMousePositionNear = new Vector3(
            Input.mousePosition.x,
            Input.mousePosition.y,
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