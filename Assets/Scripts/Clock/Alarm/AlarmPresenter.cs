using DDX.Clock.Alarm.Inputs;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace DDX.Clock.Alarm
{
    public class AlarmPresenter : MonoBehaviour
    {
        private AlarmData _alarmData;
        private List<IAlarmInput> _alarmInputs;
        private bool _isInitialized = false;

        [Inject]
        private void SetAlarmData(AlarmData alarmData)
        {
            _alarmData = alarmData;
        }

        [Inject]
        private void SetAlarmData(IEnumerable<IAlarmInput> alarmInputs)
        {
            _alarmInputs = alarmInputs.ToList();
        }

        private void Update()
        {
            if (!_isInitialized)
                Initialize();
        }

        private void Initialize()
        {
            if (_alarmInputs == null || _alarmInputs.Count == 0)
                return;
            if (_alarmData == null) return;

            foreach (var input in _alarmInputs)
            {
                input.SetAlarmValue(_alarmData.WhenAlarm);
                input.AlarmValueChanged += Input_AlarmValueChanged;
            }
            _alarmData.WhenAlarmChanged += WhenAlarmChanged;
            _isInitialized = true;
        }

        private void WhenAlarmChanged(object sender, TimeSpan e)
        {
            foreach (var input in _alarmInputs)
                input.SetAlarmValue(_alarmData.WhenAlarm);
        }

        private void Input_AlarmValueChanged(object sender, TimeSpan e)
        {
            _alarmData.WhenAlarm = e;
        }
    }
}
