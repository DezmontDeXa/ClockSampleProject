using System;
using UnityEngine;
using UnityEngine.UI;

namespace DDX.Clock.Views
{
    public class WallClockView : MonoBehaviour, ITimeView
    {
        [SerializeField] private Image _hoursHandImage;
        [SerializeField] private Image _minusteHandImage;
        [SerializeField] private Image _secondsHandImage;

        public void SetTime(TimeSpan time)
        {
            // magics:
            // 360 is degrees in a circle
            // 12 is max hours in dial
            // 60 is max minutes or seconds in dial

            var hours = (float)time.TotalHours;
            var minutes = (float)(time.TotalMinutes - Math.Floor(hours) * 60);
            var seconds = (float)(time.TotalSeconds - Math.Floor(minutes) * 60);

            var hoursHandRotZ = hours / 12 * 360;
            var minutesHandRotZ = minutes / 60 * 360;
            var secondsHandRotZ = seconds / 60 * 360;

            _hoursHandImage.rectTransform.rotation = Quaternion.Euler(0, 0, -hoursHandRotZ);
            _minusteHandImage.rectTransform.rotation = Quaternion.Euler(0, 0, -minutesHandRotZ);
            _secondsHandImage.rectTransform.rotation = Quaternion.Euler(0, 0, -secondsHandRotZ);
        }
    }
}
