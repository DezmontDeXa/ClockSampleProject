using DDX.Clock.TimeProviders.Implements;
using DDX.Clock.TimeProviders;
using UnityEngine;
using Zenject;
using DDX.Clock;
using DDX.Clock.Alarm;

namespace DDX.Zenject
{
    [CreateAssetMenu(fileName = "DefaultIInstaller", menuName = "Installers/DefaultIInstaller")]
    public class DefaultIInstaller : ScriptableObjectInstaller<DefaultIInstaller>
    {
        [SerializeField] private AlarmData _alarmData;

        public override void InstallBindings()
        {
            Container.Bind<ITimeProvider>().To<WorldTimeApiOrgTimeProvider>().AsSingle();
            Container.Bind<ITimeProvider>().To<GoogleNtpTimeProvider>().AsSingle();
            Container.Bind<ITimeProvider>().To<Google1NtpTimeProvider>().AsSingle();
            Container.Bind<ITimeProvider>().To<Google2NtpTimeProvider>().AsSingle();
            Container.Bind<ITimeProvider>().To<Google3NtpTimeProvider>().AsSingle();
            Container.Bind<ITimeProvider>().To<Google4NtpTimeProvider>().AsSingle();
            Container.Bind<WebTime>().AsSingle();
            Container.Bind<AlarmData>().FromScriptableObject(_alarmData).AsSingle();
        }
    }
}