using System.Collections.Generic;
using UnityEngine;

public class GridHandler : MonoBehaviour
{
    [Header("Grid")] [SerializeField] private Transform _gridPivot;
    [SerializeField, Min(0.01f)] private float _cellSize = 1f;

    private readonly Dictionary<Vector2Int, GameObject> _occupied = new Dictionary<Vector2Int, GameObject>();

    public Transform Pivot => _gridPivot;
    // public float CellSize => _cellSize;

    public Vector2Int WorldToCell(Vector3 world)
    {
        if (_gridPivot == null) return Vector2Int.zero;

        Vector3 local = Quaternion.Inverse(_gridPivot.rotation) * (world - _gridPivot.position);
        int x = Mathf.RoundToInt(local.x / _cellSize);
        int y = Mathf.RoundToInt(local.z / _cellSize); // используем XZ плоскость
        return new Vector2Int(x, y);
    }

    public Vector3 CellToWorld(Vector2Int cell)
    {
        if (_gridPivot == null) return Vector3.zero;

        Vector3 local = new Vector3(cell.x * _cellSize, 0f, cell.y * _cellSize);
        return _gridPivot.position + _gridPivot.rotation * local;
    }

    public Vector3 SnapToGrid(Vector3 world)
    {
        return CellToWorld(WorldToCell(world));
    }

    public bool IsCellOccupied(Vector2Int cell)
    {
        return _occupied.ContainsKey(cell);
    }

    public GameObject GetOccupant(Vector2Int cell)
    {
        if (_occupied.TryGetValue(cell, out GameObject occupant))
            return occupant;
        else
            return null;
    }

    public void ReserveCell(Vector2Int cell, GameObject occupant)
    {
        _occupied[cell] = occupant;
    }

    public void ReleaseCell(Vector2Int cell, GameObject occupant)
    {
        if (_occupied.TryGetValue(cell, out var current) && current == occupant)
            _occupied.Remove(cell);
    }

    public void RegisterAtCurrentCell(GameObject occupant)
    {
        var cell = WorldToCell(occupant.transform.position);
        ReserveCell(cell, occupant);
    }
}