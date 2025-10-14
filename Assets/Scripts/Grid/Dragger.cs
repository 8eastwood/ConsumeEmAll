using UnityEngine;

public class Dragger : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private GridHandler _gridHandler;
    [SerializeField] private Transform _gridPivot;

    [SerializeField] private DesktopInput _desktopInput;

    private GameObject _selectedObject;
    private int _draggableLayerMask = 1 << 3;
    private Vector3 _dragOffset;
    private Vector3 _velocity;
    private float _smoothTime = 0.08f;

    private void Update()
    {
        if (_desktopInput.IsPointerDown)
            HandleMouseClick();

        if (_selectedObject != null)
            UpdateSelectedObjectPosition();
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
        }
    }

    private void DropObject()
    {
        UpdateSelectedObjectPosition();
        _selectedObject = null;
        Cursor.visible = true;
    }

    private void UpdateSelectedObjectPosition()
    {
        if (_gridHandler.TryGetPosition(out Vector2 position))
        {
            Vector3 targetPosition = new Vector3(position.x, 0, position.y);
            targetPosition += _gridPivot.position;
            
            _selectedObject.transform.position = Vector3.SmoothDamp(
                _selectedObject.transform.position,
                targetPosition,
                ref _velocity,
                _smoothTime);
        }
    }

    private RaycastHit CastRay()
    {
        Camera camera = _mainCamera;

        Vector3 screenMousePositionFar = new Vector3(
            _desktopInput.PointerPosition.x,
            _desktopInput.PointerPosition.y,
            camera.farClipPlane);
        Vector3 screenMousePositionNear = new Vector3(
            _desktopInput.PointerPosition.x,
            _desktopInput.PointerPosition.y,
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
    
    private bool TryHitPlane(Ray ray, out Vector3 hitPoint)
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

    private bool IsOnDragLayer(GameObject gameObject)
    {
        return (_draggableLayerMask & (1 << gameObject.layer)) != 0;
    }
}