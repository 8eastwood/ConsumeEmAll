using System;
using UnityEngine;

public class GridHandler : MonoBehaviour
{
    [SerializeField] private Transform _gridPivot;
    [SerializeField] private Vector2Int _gridSize;
    [SerializeField] private Vector2 _cellSize;
    
    [SerializeField] private InputReader _inputReader;
    
    private Plane _plane;
    private Camera _camera;

    public Vector2 CellSize => _cellSize;

    private void Awake()
    {
        _camera = Camera.main;
        _plane = new Plane(Vector3.up, _gridPivot.position);
    }

    public bool TryGetPosition(out Vector2 position)
    {
        position = Vector2.zero;

        Ray ray = _camera.ScreenPointToRay(_inputReader.MouseScreenPosition);

        if (_plane.Raycast(ray, out float distance) == false)
            return false;

        Vector3 hitPoint = ray.GetPoint(distance);
        hitPoint -= _gridPivot.position;
        position.x = Mathf.FloorToInt(hitPoint.x / _cellSize.x);
        position.y = Mathf.FloorToInt(hitPoint.z / _cellSize.y);
        Debug.Log(position);

        return position.x >= 0 && position.x < _gridSize.x && position.y >= 0 && position.y < _gridSize.y;
    }
}