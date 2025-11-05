using UnityEngine;

[RequireComponent(typeof(Outline))]
public class Draggable : MonoBehaviour
{
    private Outline _outline;

    private void OnEnable()
    {
        _outline = GetComponent<Outline>();
        // SetOutlineOn();
    }

    public void SetOutlineOn()
    {
        _outline.OutlineWidth = 10;
    }

    public void SetOutlineOff()
    {
        _outline.OutlineWidth = 0;
    }
}