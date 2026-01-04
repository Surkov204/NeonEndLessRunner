using Zenject;

public class WeaponSignalInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        SignalBusInstaller.Install(Container);

        Container.DeclareSignal<AmmoChangedSignal>();
    }
}