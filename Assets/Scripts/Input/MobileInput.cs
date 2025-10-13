using UnityEngine;
using UnityEngine.EventSystems;

public class MobileInput : MonoBehaviour, IInput
{
    public Vector3 ScreenPosition => Input.GetTouch(0).position;

    public bool IsPointerDown => Input.touchCount > 0 &&
                                 Input.GetTouch(0).phase == TouchPhase.Began;

    public bool IsPointerHeld => Input.touchCount > 0 &&
                                 Input.GetTouch(0).phase == TouchPhase.Stationary ||
                                 Input.GetTouch(0).phase == TouchPhase.Moved;

    public bool IsPointerUp => Input.touchCount > 0 &&
                               Input.GetTouch(0).phase == TouchPhase.Ended;


    // public bool IsPointerOverUI
    // {
    //     get
    //     {
    //         if (Input.touchCount > 0)
    //         {
    //             var eventSytem = EventSystem.current;
    //             return eventSystem != null && eventSystem.IsPointerOverGameObject(Input.GetTouch(0).fingerId);
    //         }
    //
    //         return false;
    //     }
    // }
}