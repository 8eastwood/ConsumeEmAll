using TMPro;
using UnityEngine;

public class OrientationChecker : MonoBehaviour
{
    private int _screenWidth;
    private int _screenHeight;

    private void Update()
    {
        _screenWidth = Screen.width;
        _screenHeight = Screen.height;
    }

    public bool IsPortraitByResolution()
    {
        return _screenHeight > _screenWidth;
    }

    // public bool IsLandscapeByResolution()
    // {
    //     return _screenWidth > _screenHeight;
    // }
    //
    // public bool IsSquareByResolution()
    // {
    //     return _screenWidth == _screenHeight;
    // }
}