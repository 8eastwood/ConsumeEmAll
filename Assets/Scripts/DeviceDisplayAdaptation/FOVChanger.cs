using TMPro;
using UnityEngine;

public class FOVChanger : MonoBehaviour
{
    [Header("Components")] 
    [SerializeField] private DeviceTypeDetector _deviceTypeDetector;
    [SerializeField] private Camera _camera;
    [SerializeField] private OrientationChecker _orientationChecker;
    [Header("Settings")] 
    [SerializeField] private int _mobileFOV = 77;
    [SerializeField] private int _defaultFOV = 60;

    private const string MobileDevice = "mobile";

    private void Update()
    {
        TryChangeFOV();
    }

    private void TryChangeFOV()
    {
        if (IsMobileDevice(_deviceTypeDetector.DeviceType) && _orientationChecker.IsPortraitByResolution())
            _camera.fieldOfView = _mobileFOV;
        else
            _camera.fieldOfView = _defaultFOV;
    }

    private bool IsMobileDevice(string deviceType)
    {
        return deviceType.ToLower() == MobileDevice;
    }
}