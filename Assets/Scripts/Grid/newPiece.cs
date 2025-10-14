using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Piece : MonoBehaviour
{
    [Tooltip("Относительные клетки фигуры в координатах сетки (x,y,z). y — высота.")]
    public List<Vector3Int> cellsOffsets = new List<Vector3Int>()
    {
        // По умолчанию: плоский 2x2 на XZ на уровне y=0
        new Vector3Int(0, 0, 0),
        new Vector3Int(1, 0, 0),
        new Vector3Int(0, 0, 1),
        new Vector3Int(1, 0, 1),
    };

    [Tooltip("Шаги поворота по Y на 90° (0..3). Используется при вычислении габарита и для transform.rotation.")]
    [Range(0, 3)]
    public int rotationStepsY = 0;

    [Tooltip("Доп. мировой сдвиг для выравнивания пивота модели с центром клетки (обычно 0).")]
    public Vector3 pivotWorldOffset = Vector3.zero;

    // Текущая привязка пивота в клетках (управляется GridHandler).
    [HideInInspector] public Vector3Int? currentPivotCell;

    Quaternion _baseRotation;

    void Awake()
    {
        _baseRotation = transform.rotation;
        ApplyVisualRotation();
    }

    public void RotateYSteps(int deltaSteps)
    {
        rotationStepsY = (rotationStepsY + deltaSteps) & 3; // 0..3
        ApplyVisualRotation();
    }

    public void ApplyVisualRotation()
    {
        // Базовый поворот модели + кратный 90° вокруг Y
        transform.rotation = Quaternion.Euler(0f, 90f * rotationStepsY, 0f) * _baseRotation;
    }

    public IEnumerable<Vector3Int> GetRotatedOffsets()
    {
        foreach (var o in cellsOffsets)
            yield return RotateOffsetY(o, rotationStepsY);
    }

    static Vector3Int RotateOffsetY(in Vector3Int o, int steps)
    {
        steps &= 3;
        switch (steps)
        {
            case 0: return new Vector3Int(o.x, o.y, o.z);
            case 1: return new Vector3Int(o.z, o.y, -o.x);
            case 2: return new Vector3Int(-o.x, o.y, -o.z);
            case 3: return new Vector3Int(-o.z, o.y, o.x);
        }
        return o;
    }
}