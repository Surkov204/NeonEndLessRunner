using Zenject;

public class GameInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<ICurrencyService>()
                 .To<CurrencyService>()
                 .AsSingle()
                 .NonLazy();
    }
}
