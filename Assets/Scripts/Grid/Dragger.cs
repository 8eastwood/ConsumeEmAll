using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Dragger : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private GridHandler _gridHandler;
    [SerializeField] private Transform _gridPivot;

    [SerializeField] private DesktopInput _desktopInput;
    [SerializeField] private Rigidbody _rigidbody;

    private GameObject _selectedObject;

    private int _draggableLayerMask = 1 << 3;

    private Vector3 _dragOffset;
    private Vector3 _velocity;
    private float _smoothTime = 0.08f;
    private bool _isDragging = false;
    private Vector3 _targetPosition;
    private Vector3 _hitPointLocal;
    private float _positionY = 0f;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (_desktopInput.IsPointerDown)
            HandleMouseClick();

        if (_selectedObject != null)
        {
            UpdateTargetPosition();
            MoveShape();
            // UpdateSelectedObjectPosition();
        }
    }

    private void UpdateTargetPosition()
    {
        if (_gridHandler.TryGetPosition(out Vector2 position))
        {
             _targetPosition = new Vector3(position.x, _positionY, position.y);
             _targetPosition += _gridPivot.position +  _dragOffset;
             // _targetPosition += _gridPivot.position;
        }
    }

    private void MoveShape()
    {
        _selectedObject.transform.position = Vector3.SmoothDamp(
            _selectedObject.transform.position,
            _targetPosition,
            ref _velocity,
            _smoothTime);
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
            _isDragging = true;
            _rigidbody.isKinematic = false;
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.angularVelocity = Vector3.zero;
            _selectedObject = hit.collider.gameObject;
            
            Vector3 hitPointWorld = hit.point;
            _dragOffset = _selectedObject.transform.position - hitPointWorld;
        }
    }

    private void DropObject()
    {
        _isDragging = false;
        // UpdateSelectedObjectPosition();
        UpdateTargetPosition();
        _selectedObject = null;
    }

    // private void UpdateSelectedObjectPosition()
    // {
    //     if (_gridHandler.TryGetPosition(out Vector2 position))
    //     {
    //         Vector3 targetPosition = new Vector3(position.x, 0, position.y);
    //         targetPosition += _gridPivot.position;
    //
    //         _selectedObject.transform.position = Vector3.SmoothDamp(
    //             _selectedObject.transform.position,
    //             targetPosition,
    //             ref _velocity,
    //             _smoothTime);
    //     }
    // }

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

    private bool IsOnDragLayer(GameObject gameObject)
    {
        return (_draggableLayerMask & (1 << gameObject.layer)) != 0;
    }
}