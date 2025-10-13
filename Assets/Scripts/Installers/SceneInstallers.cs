using UnityEngine;
using Zenject;

public sealed class SceneInstallers : MonoInstaller
{
    public override void InstallBindings()
    {
        if (SystemInfo.deviceType == DeviceType.Desktop)
            Container.Bind<IInput>().To<DesktopInput>().AsSingle();
        else
            Container.Bind<IInput>().To<MobileInput>().AsSingle();
    }
}
