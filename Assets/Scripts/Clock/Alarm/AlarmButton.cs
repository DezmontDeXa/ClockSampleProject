using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace DDX.Clock.Alarm.Inputs
{
    [RequireComponent(typeof(Button))]
    public class AlarmButton : MonoBehaviour
    {
        private Button _button;
        private TMP_Text _buttonText;
        private IEnumerable<IAlarmView> _alarmViews;
        private bool _isActive = false;
        private AlarmData _alarmData;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _buttonText = _button.GetComponentInChildren<TMP_Text>();
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(Click);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(Click);
        }

        private void Click()
        {
            if (!_isActive)
            {
                _isActive = true;
                foreach (var alarm in _alarmViews)
                    alarm.Show();
                _buttonText.text = "Save alarm";
                _alarmData.WhenAlarmChanged += WhenAlarmDataChanged;
                _button.interactable = _alarmData.WhenAlarm != null;
            }
            else
            {
                _isActive = false;
                foreach (var alarm in _alarmViews)
                    alarm.Hide();
                _buttonText.text = "Set alarm";
                _alarmData.WhenAlarmChanged -= WhenAlarmDataChanged;
            }
        }

        private void WhenAlarmDataChanged(object sender, TimeSpan? e)
        {
            _button.interactable = _alarmData.WhenAlarm != null;
        }

        [Inject]
        private void SetAlarmViews(IEnumerable<IAlarmView> alarms)
        {
            _alarmViews = alarms;
        }

        [Inject]
        private void SetAlarmData(AlarmData alarmData)
        {
            _alarmData = alarmData;
        }
    }
}
