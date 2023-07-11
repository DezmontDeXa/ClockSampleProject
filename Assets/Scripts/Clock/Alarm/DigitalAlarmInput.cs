using System;
using TMPro;
using UnityEngine;

namespace DDX.Clock.Alarm.Inputs
{
    public class DiditalAlarmInput : MonoBehaviour, IAlarmInput
    {
        [SerializeField] private TMP_InputField _inputField;

        public event EventHandler<TimeSpan?> AlarmValueChanged;

        private void Awake()
        {
            _inputField.onValueChanged.AddListener(InputValueChanged);
            _inputField.onValidateInput = ValidateInput;
        }

        public void SetAlarmValue(TimeSpan? time)
        {
            if (enabled)
            {
                if (time == null) 
                    return;

                if (time.Value.Hours > 9)
                    _inputField.text = time?.ToString("hh\\:mm");
                else
                    _inputField.text = time?.ToString("h\\:mm");
            }
        }

        private void InputValueChanged(string value)
        {
            if (value == null || value.Length == 0)
                return;

            var groups = value.Split(':');
            if (groups.Length == 1)
            {
                var hours = int.Parse(groups[0]);
                if (hours > 23)
                {
                    groups[0] = "23";
                    _inputField.text = "23";
                }
            }
            if (groups.Length == 2)
            {
                var mins = groups[1];
                if (mins.Length == 2)
                {
                    var min = int.Parse(mins);
                    if (min > 59)
                    {
                        groups[1] = "59";
                        value = string.Join(":", groups);
                        _inputField.text = value;
                        _inputField.text = value;
                    }
                }
                else return;
            }

            value = string.Join(":", groups);
            _inputField.text = value;

            if (TimeSpan.TryParse(value, out var ts))
            {
                if (ts.TotalHours < 24)
                {
                    AlarmValueChanged?.Invoke(this, ts);
                    return;
                }
            }

            AlarmValueChanged?.Invoke(this, null);
        }

        private char ValidateInput(string text, int charIndex, char addedChar)
        {
            if (text.Length == 2)
                return ':';
            else
                if (!char.IsDigit(addedChar))
                return '\0';

            return addedChar;
        }

    }
}
