using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class physicsMover : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Camera _camera;
    [SerializeField] private float _coefficientAttractionToMouse = 10f;
    [SerializeField] private float _maxAcceleration = 40f;
    [SerializeField] private float _damp = 2f;

    private float _planeY = 0f;
    private bool _holdToDrag = true;
    private Vector3 _target;
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
        Vector3 offset = _target - _rigidbody.position;
        Vector3 desiredVelocity = (offset * _coefficientAttractionToMouse) -
                                  (_rigidbody.velocity * _damp);

        Vector3 acceleration = Vector3.ClampMagnitude(desiredVelocity, _maxAcceleration);

        _rigidbody.AddForce(acceleration, ForceMode.Acceleration);
    }
}