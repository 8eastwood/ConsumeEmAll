using UnityEngine;
using Zenject;

public class Dragger : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private GridHandler _gridHandler;
    [SerializeField] private Transform _gridPivot;

    [SerializeField] private DesktopInput _desktopInput;

    private GameObject _selectedObject;
    private int _draggableLayerMask = 1 << 3;
    private Vector3 _dragOffset;

    private void Update()
    {
        if (_desktopInput.IsPointerDown)
            HandleMouseClick();

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
        RaycastHit hit = CastRay();

        if (hit.collider != null && IsOnDragLayer(hit.collider.gameObject))
        {
            _selectedObject = hit.collider.gameObject;
            // Vector3 mouseWorldPos = GetMouseWorldPosition();
            // _dragOffset = _selectedObject.transform.position - mouseWorldPos;

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
            _selectedObject.transform.position = new Vector3(position.x, 0,
                position.y);

            _selectedObject.transform.position += _gridPivot.position;
        }
    }

    // private Vector3 GetMouseWorldPosition()
    // {
    //     Ray ray = _mainCamera.ScreenPointToRay(_inputReader.MouseScreenPosition);
    //     Plane plane = new Plane(Vector3.up, _gridPivot.position);
    //     
    //     if (plane.Raycast(ray, out float distance))
    //     {
    //         return ray.GetPoint(distance);
    //     }
    //     
    //     return _selectedObject.transform.position;
    // }

    private RaycastHit CastRay()
    {
        Camera camera = _mainCamera;

        Vector3 screenMousePositionFar = new Vector3(
            _desktopInput.ScreenPosition.x,
            _desktopInput.ScreenPosition.y,
            camera.farClipPlane);
        Vector3 screenMousePositionNear = new Vector3(
            _desktopInput.ScreenPosition.x,
            _desktopInput.ScreenPosition.y,
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