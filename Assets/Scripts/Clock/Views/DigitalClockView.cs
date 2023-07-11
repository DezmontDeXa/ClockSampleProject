using System;
using TMPro;
using UnityEngine;

namespace DDX.Clock.Views
{
    public class DigitalClockView : MonoBehaviour, ITimeView
    {
        [SerializeField] private TMP_Text _timeText;
        public void SetTime(TimeSpan time)
        {
            _timeText.text = time.ToString("hh\\:mm\\:ss");
        }
    }
}
