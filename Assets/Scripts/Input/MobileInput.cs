using UnityEngine;
using UnityEngine.EventSystems;     

public class MobileInput : MonoBehaviour, IInput
{
    public Vector3 ScreenPosition
    {
        get
        {
            if (Input.touchCount > 0)
            {
                return Input.GetTouch(0).position;
            }

            return Vector3.zero;
        }
    }

    public bool IsPointerDown
    {
        get
        {
            if (Input.touchCount > 0)
            {
                return Input.GetTouch(0).phase == TouchPhase.Began;
            }

            return false;
        }
    }

    public bool IsPointerHeld
    {
        get
        {
            if (Input.touchCount > 0)
            {
                var phase = Input.GetTouch(0).phase;

                return phase == TouchPhase.Stationary || phase == TouchPhase.Moved;
            }

            return false;
        }
    }

    public bool IsPointerUp
    {
        get
        {
            if (Input.touchCount > 0)
            {
                return Input.GetTouch(0).phase == TouchPhase.Ended;
            }

            return false;
        }
    }

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