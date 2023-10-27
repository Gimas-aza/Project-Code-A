using UnityEngine;
using Zenject;

public class BootstrapInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        BindCamera();
    }

    private void BindCamera()
    {
        Container
            .Bind<Camera>()
            .FromComponentInHierarchy()
            .AsSingle()
            .NonLazy();
    }
}