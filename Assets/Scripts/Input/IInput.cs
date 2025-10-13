using UnityEngine;

public interface IInput
{
    public Vector3 ScreenPosition { get; }
    public bool IsPointerDown { get; }
    public bool IsPointerHeld { get; }
    public bool IsPointerUp { get; }
    
    // public bool IsPointerOverUI { get; }
}
