using System;
using UnityEngine;

public class Shape : MonoBehaviour
{
    [SerializeField] private Transform shapeTransform;
    
    private int[,] _shapePlacement = new int[4,2];

    private void Start()
    {
        CalculateShapePlacement();
        DebugShapePlacementVisual();
    }

    private void CalculateShapePlacement()
    {
        Vector3 worldPosition = shapeTransform.position;
        float cellSize = 1f;
        
        int pivotX = Mathf.RoundToInt(worldPosition.x / cellSize);
        int pivotY = Mathf.RoundToInt(worldPosition.y / cellSize);

        int startX = pivotX - 1;
        int startY = pivotY - 1;

        _shapePlacement[0, 0] = startX;
        _shapePlacement[0, 1] = startY;
        _shapePlacement[1, 0] = startX + 1;
        _shapePlacement[1, 1] = startY;
        _shapePlacement[2, 0] = startX;
        _shapePlacement[2, 1] = startY + 1;
        _shapePlacement[3, 0] = startX + 1;
        _shapePlacement[3, 1] = startY + 1;
    }
    
    private void DebugShapePlacementVisual()
    {
        for (int i = 0; i < 4; i++)
        {
            int x = _shapePlacement[i, 0];
            int y = _shapePlacement[i, 1];
            Debug.Log($"({x}, {y})");
        }
    }
}
