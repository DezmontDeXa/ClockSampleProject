using UnityEngine;
using Zenject;

namespace DDX.Clock.Alarm
{

    [RequireComponent(typeof(AudioSource))]
    public class AlarmAudioPlayer : MonoBehaviour
    {
        private AudioSource _source;
        [Inject] private CurrentTime _currentTime;
        [Inject] private AlarmData _alarmData;

        private void Awake()
        {
            _source = GetComponent<AudioSource>();
        }

        private void Update()
        {
            if (_alarmData.IsRunned)
            {
                if (_currentTime.Time > _alarmData.WhenAlarm)
                {
                    _source.PlayOneShot(_alarmData.AlarmSound);
                    _alarmData.IsRunned = false;
                    Debug.Log("Alarm");
                }
            }
        }
    }
}
