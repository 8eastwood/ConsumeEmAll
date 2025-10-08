using UnityEngine;

public class InputReader : MonoBehaviour
{
    private const int MouseLeftButton = 0;
    public Vector3 MouseScreenPosition => Input.mousePosition;
    public bool LeftMouseButtonDown => Input.GetMouseButtonDown(MouseLeftButton);

}
