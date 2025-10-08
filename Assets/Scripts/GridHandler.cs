using System;
using UnityEngine;

public class GridHandler : MonoBehaviour
{
    [SerializeField] private Transform _gridPivot;
    [SerializeField] private Vector2Int _gridSize;
    [SerializeField] private Vector2 _cellSize;
    
    [SerializeField] private GameObject _draggableObject;
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
        // hitPoint += _gridPivot.position;
        position.x = Mathf.FloorToInt(hitPoint.x / _cellSize.x);
        position.y = Mathf.FloorToInt(hitPoint.z / _cellSize.y);

        // return true;
        return position.x >= 0 && position.x < _gridSize.x && position.y >= 0 && position.y < _gridSize.y;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 totalGridSize = new Vector3(_gridSize.x * _cellSize.x, 0.1f, _gridSize.y * _cellSize.y);
        Gizmos.DrawWireCube(_draggableObject.transform.position, new Vector3 (_cellSize.x,  0.1f, _cellSize.y));
    
        Gizmos.color = Color.cyan * 0.7f;
        
        for (int x = 1; x < _gridSize.x; x++)
        {
            Vector3 start = new Vector3(x * _cellSize.x, 0, 0);
            Vector3 end = new Vector3(x * _cellSize.x, 0, totalGridSize.z);
            Gizmos.DrawLine(start, end);
        }
    
        for (int y = 1; y < _gridSize.y; y++)
        {
            Vector3 start = new Vector3(0, 0, y * _cellSize.y);
            Vector3 end = new Vector3(totalGridSize.x, 0, y * _cellSize.y);
            Gizmos.DrawLine(start, end);
        }
    }
}