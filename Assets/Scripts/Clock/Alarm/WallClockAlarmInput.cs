using DDX.Clock.Views;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DDX.Clock.Alarm.Inputs
{
    public class WallClockAlarmInput : MonoBehaviour, IAlarmInput
    {
        [SerializeField] private WallClockView _alarmWallClockView;
        private RectTransform _rectTransform;
        private ClockHand _currentClockHand;
        private TimeSpan? _currentAlarmTime;
        private int _hours;

        public event EventHandler<TimeSpan?> AlarmValueChanged;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
        }

        private void Update()
        {
            if (Input.touchCount > 0)
            {
                foreach (var touch in Input.touches)
                {
                    if (touch.phase == TouchPhase.Began)
                    {
                        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(_rectTransform, touch.position, Camera.main, out var localPoint))
                        {
                            var center = _rectTransform.rect.center;
                            var d = center - localPoint;
                            double angle = Math.Atan2(d.x, -d.y) * (180 / Math.PI);

                            if (angle > 0)
                                angle = 360 - angle;
                            else
                                angle *= -1;

                            SelectNearbyClockHand(angle);
                            StartMoveArrow(angle);
                        }
                    }
                }
            }
        }

        public void SetAlarmValue(TimeSpan? time)
        {
            if (time == null)
                time = new TimeSpan();

            _alarmWallClockView.SetTime(time.Value);
            _currentAlarmTime = time;
        }

        private void SelectNearbyClockHand(double angle)
        {
            var hourlyHandAngle = _currentAlarmTime.Value.Hours / 12 * 360;
            var minutsHandAngle = _currentAlarmTime.Value.Hours / 60 * 360;

            if (Math.Abs(hourlyHandAngle - angle) > Math.Abs(minutsHandAngle - angle))
                _currentClockHand = ClockHand.Minutes;
            else
                _currentClockHand = ClockHand.Hourly;
        }

        private void StartMoveArrow(double angle)
        {
            //angle / 360 * 12
            var minutes = (int)(angle / 360 * 60);
            var hours = (int)(angle / 360 * 12);

            switch (_currentClockHand)
            {
                case ClockHand.Minutes:
                    SetAlarmValue(new TimeSpan(_hours, minutes, 0));
                    break;
                case ClockHand.Hourly:
                    SetAlarmValue(new TimeSpan(_hours, minutes, 0));
                    break;
            }
        }

        //public void OnPointerClick(PointerEventData eventData)
        //{
        //    if (RectTransformUtility.ScreenPointToLocalPointInRectangle(_rectTransform, eventData.position, Camera.main, out var localPoint))
        //    {
        //        var center = _rectTransform.rect.center;

        //        Vector2 d = center - localPoint;

        //        double angle = Math.Atan2(d.x, -d.y) * (180 / Math.PI);

        //        if (angle > 0)
        //            angle = 360 - angle;
        //        else
        //            angle *= -1;

        //        Set(angle);
        //    }
        //}

        private enum ClockHand
        {
            Hourly,
            Minutes
        }
    }
}
