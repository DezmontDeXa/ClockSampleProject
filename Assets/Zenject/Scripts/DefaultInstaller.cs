using DDX.Clock.TimeProviders.Implements;
using DDX.Clock.TimeProviders;
using UnityEngine;
using Zenject;
using DDX.Clock;

namespace DDX.Zenject
{
    [CreateAssetMenu(fileName = "DefaultIInstaller", menuName = "Installers/DefaultIInstaller")]
    public class DefaultIInstaller : ScriptableObjectInstaller<DefaultIInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<ITimeProvider>().To<WorldTimeApiOrgTimeProvider>().AsSingle();
            Container.Bind<ITimeProvider>().To<GoogleNtpTimeProvider>().AsSingle();
            Container.Bind<ITimeProvider>().To<Google1NtpTimeProvider>().AsSingle();
            Container.Bind<ITimeProvider>().To<Google2NtpTimeProvider>().AsSingle();
            Container.Bind<ITimeProvider>().To<Google3NtpTimeProvider>().AsSingle();
            Container.Bind<ITimeProvider>().To<Google4NtpTimeProvider>().AsSingle();
            Container.Bind<WebTime>().AsSingle();
        }
    }
}