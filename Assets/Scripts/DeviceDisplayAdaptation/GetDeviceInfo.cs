using YG;
using UnityEngine;

public class DeviceTypeDetector : MonoBehaviour
{
    public string DeviceType { get; private set; }

    private void Start()
    {
        GetDeviceType();
        
    }
    
    private void GetDeviceType()
    {
        DeviceType = YG2.envir.deviceType;
    }
}
