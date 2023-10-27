using UnityEngine;
using Zenject;

public class ObjectPoolInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        BindBulletPool();
    }

    private void BindBulletPool()
    {
        Container
            .Bind<BulletPool>()
            .FromComponentInHierarchy()
            .AsSingle()
            .NonLazy();
    }
}