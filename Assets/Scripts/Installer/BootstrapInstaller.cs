using Assets.UI;
using Assets.Units;
using Assets.Units.Player;
using UnityEngine;
using Zenject;

public class BootstrapInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        BindPlayer();
        BindUIVatilityMonitor();
        BindCamera();
        BindPlayerSkills();
    }

    private void BindPlayer()
    {
        Container
            .Bind<PlayerUnit>()
            .FromComponentInHierarchy()
            .AsSingle()
            .NonLazy();
    }

    private void BindUIVatilityMonitor()
    {
        Container
            .Bind<VitalityMonitor>()
            .FromComponentInHierarchy()
            .AsSingle()
            .NonLazy();
    }

    private void BindCamera()
    {
        Container
            .Bind<Camera>()
            .FromComponentInHierarchy()
            .AsSingle()
            .NonLazy();
    }

    private void BindPlayerSkills()
    {
        Container
            .Bind<PlayerSkills>()
            .AsSingle();
    }
}