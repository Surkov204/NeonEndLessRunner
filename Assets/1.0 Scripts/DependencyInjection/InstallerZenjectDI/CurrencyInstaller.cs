using Zenject;

public class CurrencyInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<ICurrencyService>()
                 .To<CurrencyService>()
                 .AsSingle()
                 .NonLazy();
    }
}
