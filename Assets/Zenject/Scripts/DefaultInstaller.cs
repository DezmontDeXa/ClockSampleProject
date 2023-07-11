using DDX.Clock.TimeProviders.Implements;
using DDX.Clock.TimeProviders;
using UnityEngine;
using Zenject;

namespace DDX.Zenject
{
    [CreateAssetMenu(fileName = "DefaultIInstaller", menuName = "Installers/DefaultIInstaller")]
    public class DefaultIInstaller : ScriptableObjectInstaller<DefaultIInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<ITimeProvider>().To<WorldTimeApiOrgTimeProvider>();
            Container.Bind<ITimeProvider>().To<GoogleNtpTimeProvider>();
            Container.Bind<ITimeProvider>().To<Google1NtpTimeProvider>();
            Container.Bind<ITimeProvider>().To<Google2NtpTimeProvider>();
            Container.Bind<ITimeProvider>().To<Google3NtpTimeProvider>();
            Container.Bind<ITimeProvider>().To<Google4NtpTimeProvider>();
        }
    }
}