using System;

namespace DDX.Clock.Alarm.Inputs
{
    public interface IAlarmInput
    {
        event EventHandler<TimeSpan?> AlarmValueChanged;

        void SetAlarmValue(TimeSpan? time);
    }
}
