using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-100)]
public class newGridHandler : MonoBehaviour
{
    [Header("Grid Geometry")]
    [Tooltip("Мировые координаты центра клетки (0,0,0).")]
    public Vector3 origin = Vector3.zero;

    [Tooltip("Размер сетки в клетках (X, Y, Z). Для плоскости используйте Y=1.")]
    public Vector3Int size = new Vector3Int(10, 1, 10);

    [Tooltip("Физический размер клетки в мире (по осям X,Y,Z).")]
    public Vector3 cellSize = Vector3.one;

    [Header("Height (Y) handling")]
    [Tooltip("Фиксировать все перетаскивания на уровне Y = fixedYLayer.")]
    public bool lockYToFixedLayer = true;

    [Tooltip("Слой по Y (в клетках), на котором размещаются фигуры при перетаскивании.")]
    public int fixedYLayer = 0;

    [Header("Bootstrapping")]
    [Tooltip("Попробовать зарегистрировать все Piece на старте по их текущей позиции.")]
    public bool autoRegisterAtStart = true;

    // Карта занятости: клетка -> фигура
    readonly Dictionary<Vector3Int, Piece> _occupied = new Dictionary<Vector3Int, Piece>();
    // Обратная карта: фигура -> множество её клеток
    readonly Dictionary<Piece, HashSet<Vector3Int>> _pieceCells = new Dictionary<Piece, HashSet<Vector3Int>>();

    void Start()
    {
        if (!autoRegisterAtStart) return;

        var pieces = FindObjectsOfType<Piece>();
        foreach (var p in pieces)
        {
            var cell = WorldToCellRound(p.transform.position - p.pivotWorldOffset);
            if (lockYToFixedLayer)
                cell.y = Mathf.Clamp(fixedYLayer, 0, size.y - 1);

            if (IsPlacementValid(p, cell))
            {
                Occupy(p, cell, snapTransform: true);
            }
            else
            {
                Debug.LogWarning($"[GridHandler] Не удалось зарегистрировать {p.name} в {cell} — пересечение или вне границ.");
            }
        }
    }

    public Vector3Int WorldToCellRound(Vector3 world)
    {
        Vector3 local = world - origin;
        int cx = Mathf.RoundToInt(local.x / Mathf.Max(cellSize.x, Mathf.Epsilon));
        int cy = Mathf.RoundToInt(local.y / Mathf.Max(cellSize.y, Mathf.Epsilon));
        int cz = Mathf.RoundToInt(local.z / Mathf.Max(cellSize.z, Mathf.Epsilon));
        return new Vector3Int(cx, cy, cz);
    }

    public Vector3 CellToWorld(Vector3Int cell)
    {
        Vector3 scaled = new Vector3(cell.x * cellSize.x, cell.y * cellSize.y, cell.z * cellSize.z);
        return origin + scaled;
    }

    public float GetWorldYForLayer(int yLayer)
    {
        return origin.y + yLayer * cellSize.y;
    }

    public bool IsInsideBounds(Vector3Int cell)
    {
        return cell.x >= 0 && cell.y >= 0 && cell.z >= 0 &&
               cell.x < size.x && cell.y < size.y && cell.z < size.z;
    }

    public IEnumerable<Vector3Int> GetCellsFor(Piece piece, Vector3Int pivotCell)
    {
        foreach (var o in piece.GetRotatedOffsets())
            yield return pivotCell + o;
    }

    public bool IsPlacementValid(Piece piece, Vector3Int pivotCell)
    {
        foreach (var c in GetCellsFor(piece, pivotCell))
        {
            if (!IsInsideBounds(c)) return false;

            if (_occupied.TryGetValue(c, out var other))
            {
                if (other != piece) return false; // занято чужим
            }
        }
        return true;
    }

    public void Occupy(Piece piece, Vector3Int pivotCell, bool snapTransform)
    {
        // Сначала очистить прежние клетки
        Release(piece);

        // Заполнить новыми
        var cells = new HashSet<Vector3Int>();
        foreach (var c in GetCellsFor(piece, pivotCell))
        {
            _occupied[c] = piece;
            cells.Add(c);
        }
        _pieceCells[piece] = cells;
        piece.currentPivotCell = pivotCell;

        if (snapTransform)
        {
            var targetPos = CellToWorld(pivotCell) + piece.pivotWorldOffset;
            piece.transform.position = targetPos;
            // Визуальный поворот уже в Piece.ApplyVisualRotation()
        }
    }
    
    public void Release(Piece piece)
    {
        if (_pieceCells.TryGetValue(piece, out var cells))
        {
            foreach (var c in cells)
                if (_occupied.TryGetValue(c, out var p) && p == piece)
                    _occupied.Remove(c);

            _pieceCells.Remove(piece);
        }
    }

    void OnDrawGizmosSelected()
    {
        // Рисуем рамку сетки
        Gizmos.color = Color.cyan;
        Vector3 center = origin + Vector3.Scale(new Vector3(size.x - 1, size.y - 1, size.z - 1), cellSize) * 0.5f;
        Vector3 totalSize = Vector3.Scale((Vector3)size, cellSize);
        Gizmos.DrawWireCube(center, totalSize);

        // Плоскость перетаскивания
        if (lockYToFixedLayer)
        {
            Gizmos.color = new Color(0f, 1f, 0f, 0.25f);
            float y = GetWorldYForLayer(Mathf.Clamp(fixedYLayer, 0, Mathf.Max(0, size.y - 1)));
            Vector3 planeCenter = new Vector3(origin.x + (size.x - 1) * cellSize.x * 0.5f, y, origin.z + (size.z - 1) * cellSize.z * 0.5f);
            Vector3 planeSize = new Vector3(size.x * cellSize.x, 0.001f, size.z * cellSize.z);
            Gizmos.DrawCube(planeCenter, planeSize);
        }
    }
}

