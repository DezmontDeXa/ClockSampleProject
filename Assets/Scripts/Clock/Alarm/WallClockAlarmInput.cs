using DDX.Clock.Views;
using System;
using UnityEngine;

namespace DDX.Clock.Alarm.Inputs
{
    public class WallClockAlarmInput : MonoBehaviour, IAlarmInput
    {
        [SerializeField] private WallClockView _alarmWallClockView;

        public event EventHandler<TimeSpan?> AlarmValueChanged;

        public void SetAlarmValue(TimeSpan? time)
        {
            if (time == null)
                time = new TimeSpan();

            _alarmWallClockView.SetTime(time.Value);
        }
    }
}
