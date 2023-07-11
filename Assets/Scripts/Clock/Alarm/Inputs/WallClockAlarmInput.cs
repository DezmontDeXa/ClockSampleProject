using DDX.Clock.Views;
using System;
using UnityEngine;

namespace DDX.Clock.Alarm.Inputs
{
    public class WallClockAlarmInput : MonoBehaviour, IAlarmInput
    {
        [SerializeField] private WallClockView _alarmWallClockView;
        private RectTransform _rectTransform;
        private ClockHand _currentClockHand;
        private TimeSpan? _currentAlarmTime;
        private int _hours;
        private int _minutes;
        private bool _drag = false;

        public event EventHandler<TimeSpan> AlarmValueChanged;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
        }

        private void Update()
        {
            if (!RectTransformUtility.RectangleContainsScreenPoint(_rectTransform, Input.mousePosition, Camera.main))
                return;


            if (Input.GetMouseButtonDown(0))
                StartDrag();

            if (Input.GetMouseButtonUp(0))
                StopDrag();

            if (_drag)
                ProcessDrag();
        }

        private void StartDrag()
        {
            var angle = GetPointAngle(Input.mousePosition);
            if (angle == null) return;
            _currentClockHand = SelectNearbyClockHand(angle.Value);
            AlarmValueChanged?.Invoke(this, TimeSpan.Zero);
            _drag = true;
        }

        private void StopDrag()
        {
            _drag = false;
        }

        private void ProcessDrag()
        {
            var angle = GetPointAngle(Input.mousePosition);
            var minutes = (int)(angle / 360 * 60);
            var hours = (int)(angle / 360 * 12);

            TimeSpan time = new TimeSpan();
            switch (_currentClockHand)
            {
                case ClockHand.Hourly:
                    time = new TimeSpan(hours, _minutes, 0);
                    _hours = hours;
                    break;
                case ClockHand.Minutes:
                    time = new TimeSpan(_hours, minutes, 0);
                    _minutes = minutes;
                    break;
            }

            _alarmWallClockView.SetTime(time);
            _currentAlarmTime = time;
            AlarmValueChanged?.Invoke(this, time);
        }

        private float? GetPointAngle(Vector2 point)
        {
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(_rectTransform, point, Camera.main, out var localPoint))
            {
                var center = _rectTransform.rect.center;
                var d = center - localPoint;
                double angle = Math.Atan2(d.x, -d.y) * (180 / Math.PI);

                if (angle > 0)
                    angle = 360 - angle;
                else
                    angle *= -1;

                return (float)angle;
            }
            return null;
        }

        public void SetAlarmValue(TimeSpan time)
        {
            _alarmWallClockView.SetTime(time);
            _currentAlarmTime = time;
        }

        private ClockHand SelectNearbyClockHand(double angle)
        {
            var hourlyHandAngle = (float)_currentAlarmTime.Value.Hours / 12 * 360;
            var minutsHandAngle = (float)_currentAlarmTime.Value.Minutes / 60 * 360;

            if (Math.Abs(hourlyHandAngle - angle) > Math.Abs(minutsHandAngle - angle))
                return ClockHand.Minutes;
            else
                return ClockHand.Hourly;
        }

        private enum ClockHand
        {
            Hourly,
            Minutes
        }
    }
}
