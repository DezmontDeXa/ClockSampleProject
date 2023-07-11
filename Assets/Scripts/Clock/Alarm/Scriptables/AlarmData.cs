using System;
using UnityEngine;

namespace DDX.Clock.Alarm
{
    [CreateAssetMenu(fileName = "AlarmData")]
    public class AlarmData : ScriptableObject
    {
        [SerializeField] private AudioClip _alarmSound;

        private TimeSpan _whenAlarm;

        public TimeSpan WhenAlarm
        {
            get => _whenAlarm;
            set
            {
                _whenAlarm = value;
                WhenAlarmChanged?.Invoke(this, _whenAlarm);
            }
        }

        public AudioClip AlarmSound => _alarmSound;

        public bool IsRunned { get; set; }


        public event EventHandler<TimeSpan> WhenAlarmChanged;
    }

}