using UnityEngine;
using Zenject;

public sealed class SceneInstallers : MonoInstaller
{
    [SerializeField] private DesktopInput _desktopInput;
    
    public override void InstallBindings()
    {
        // if (SystemInfo.deviceType == DeviceType.Desktop)
        //     Container.BindInterfacesAndSelfTo<DesktopInput>().AsSingle();
        // else
        //     Container.BindInterfacesAndSelfTo<MobileInput>().AsSingle();
        
            Container.Bind<DesktopInput>().FromComponentInNewPrefab(_desktopInput).AsSingle();
       Debug.Log("we got here");
    }
}
