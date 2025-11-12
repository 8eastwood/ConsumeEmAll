using System.Collections;
using UnityEngine;

public class Dragger : MonoBehaviour
{
    [SerializeField] private AnimationCurve _snapCurve;
    [SerializeField] private DesktopInput _desktopInput;
    [SerializeField] private GridHandler _gridHandler;
    [SerializeField] private LayerMask _draggableLayerMask;
    [SerializeField] private Transform _gridPivot;
    [SerializeField] private float _snapTime = .3f;
    [SerializeField] private float _liftSelectedObject = .05f;

    private GameObject _selectedObject;
    private Plane _plane;
    private Vector3 _dragOffset;
    private Vector3 _velocity;
    private Vector3 _targetPosition;
    private bool _isDragging = false;
    private float _maxLength = 10.0f;
    private float _speedMultiplier = 10f;
    private Draggable _draggable;

    private void Awake()
    {
        _plane = new Plane(Vector3.up, _gridPivot.position);
    }

    private void Update()
    {
        if (_isDragging == false)
            HandleDefaultState();
        else
            HandleDraggingState();
    }

    private void HandleDefaultState()
    {
        if (_desktopInput.IsPointerDown && TrySelectObject())
            _isDragging = true;
    }

    private void HandleDraggingState()
    {
        if (_desktopInput.IsPointerUp)
        {
            _isDragging = false;
            StartCoroutine(SnappingToGrid());
            return;
        }

        UpdateTargetPosition();
        MoveShape();
    }

    private bool TrySelectObject()
    {
        var ray = Camera.main.ScreenPointToRay(_desktopInput.PointerPosition);
        Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, _draggableLayerMask);

        if (hit.collider == null || !hit.collider.gameObject.TryGetComponent<Rigidbody>(out var rb))
            return false;

        // Debug.Log(hit.collider.name);
        rb.isKinematic = false;
        rb.velocity = Vector3.zero;
        _selectedObject = hit.collider.gameObject;
        _draggable = _selectedObject.GetComponent<Draggable>();
        _selectedObject.transform.position += new Vector3(0f, _liftSelectedObject, 0f);
        _dragOffset = _selectedObject.transform.position - CastRayToPlane(ray);

        return true;
    }

    private void UpdateTargetPosition()
    {
        Vector3 castedPoint = CastRayToPlane(Camera.main.ScreenPointToRay(_desktopInput.PointerPosition));
        Vector3 offsetPoint = castedPoint + _dragOffset;

        if (_gridHandler.TryGetValidGridPosition(castedPoint, out Vector2 position))
            _targetPosition = offsetPoint;
    }

    private void MoveShape()
    {
        _selectedObject.TryGetComponent<Rigidbody>(out var rigidbody);
        rigidbody.velocity = (_targetPosition - _selectedObject.transform.position) * _speedMultiplier;

        rigidbody.velocity = Vector3.ClampMagnitude(rigidbody.velocity, _maxLength);
    }

    private IEnumerator SnappingToGrid()
    {
        GameObject temporaryObject = _selectedObject;
        _selectedObject = null;
        _draggable = null;
        temporaryObject.TryGetComponent<Rigidbody>(out var rb);
        rb.isKinematic = true;

        _gridHandler.TryGetValidGridPosition(temporaryObject.transform.position, out Vector2 position);
        Vector3 start = temporaryObject.transform.position;
        Vector3 end = _gridPivot.position + new Vector3(position.x + .5f, 0, position.y + .5f);

        for (float t = 0f; t < 1f; t += Time.deltaTime / _snapTime)
        {
            temporaryObject.transform.position = Vector3.Lerp(start, end, _snapCurve.Evaluate(t));
            yield return null;
        }

        temporaryObject.transform.position = end;
    }

    private Vector3 CastRayToPlane(Ray ray)
    {
        _plane.Raycast(ray, out float distance);
        Vector3 hitPoint = ray.GetPoint(distance);
        return hitPoint;
    }
}