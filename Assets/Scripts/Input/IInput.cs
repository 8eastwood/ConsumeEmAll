using UnityEngine;

public interface IInput
{
    public Vector3 PointerPosition { get; }
    public bool IsPointerDown { get; }
    public bool IsPointerHeld { get; }
    public bool IsPointerUp { get; }
    
    // public bool IsPointerOverUI { get; }
}
