using Zenject;

public class CoinUIInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<CoinFlyUI>()
                 .FromComponentInHierarchy()
                 .AsSingle();
    }
}
