using UnityEngine;

public class DesktopInput : MonoBehaviour, IInput
{
    private const int MouseLeftButton = 0;
    public Vector3 ScreenPosition => Input.mousePosition;
    public bool IsPointerDown => Input.GetMouseButtonDown(MouseLeftButton);
    public bool IsPointerHeld => Input.GetMouseButton(MouseLeftButton);
    public bool IsPointerUp => Input.GetMouseButtonUp(MouseLeftButton);
}
