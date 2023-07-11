using System;
using UnityEngine;

namespace DDX.Clock.Alarm
{
    [CreateAssetMenu(fileName = "AlarmData")]
    public class AlarmData : ScriptableObject
    {
        private TimeSpan? _whenAlarm;

        public TimeSpan? WhenAlarm
        {
            get => _whenAlarm;
            set
            {
                _whenAlarm = value;
                if(value!=null)
                    Debug.Log(value);

                WhenAlarmChanged?.Invoke(this, _whenAlarm);
            }
        }


        public event EventHandler<TimeSpan?> WhenAlarmChanged;
    }

}