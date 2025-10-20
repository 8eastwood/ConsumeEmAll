using UnityEngine;

public class velocityMover : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Camera _camera;

    private Vector3 _target;
    private float _planeY = 0f;
    private bool _holdToDrag = true;
    private float _coefficient = 8f;
    private float _maxSpeed = 12f;
    private float _lerp = 0.5f;
    private bool _hasTarget;

    private void Update()
    {
        if (_holdToDrag && !Input.GetMouseButton(0))
        {
            _hasTarget = false;
            return;
        }

        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, new Vector3(0f, _planeY, 0f));

        if (plane.Raycast(ray, out float distance))
        {
            _target = ray.GetPoint(distance);
            _hasTarget = true;
        }
    }

    private void FixedUpdate()
    {
        Vector3 toTarget = _target - _rigidbody.position;
        Vector3 desiredVelocity = toTarget * _coefficient;
        desiredVelocity.y = _rigidbody.velocity.y;
        Vector3 horizontal = new Vector3(desiredVelocity.x, 0f, desiredVelocity.z);

        if (horizontal.magnitude > _maxSpeed)
            horizontal = horizontal.normalized * _maxSpeed;

        Vector3 finalVelocity = horizontal + Vector3.up * desiredVelocity.y;

        _rigidbody.velocity = Vector3.Lerp(_rigidbody.velocity, finalVelocity, Mathf.Clamp01(_lerp));
    }
}