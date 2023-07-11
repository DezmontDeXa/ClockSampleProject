using DDX.Clock.Views;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace DDX.Clock
{
    public class CurrentTimePresenter : MonoBehaviour
    {
        private CurrentTime _currentTime;
        private IEnumerable<ITimeView> _views;

        [Inject]
        public void SetCurrentTime(CurrentTime currentTime)
        {
            _currentTime = currentTime;
            if (_views != null)
                Init();
        }

        [Inject]
        public void SetViews(IEnumerable<ITimeView> views)
        {
            _views = views;
            if (_currentTime != null)
                Init();
        }

        private void Init()
        {
            _currentTime.TimeChanged += (s, e) => TimeChanged(e, _views);
        }

        private void TimeChanged(TimeSpan time, IEnumerable<ITimeView> views)
        {
            foreach (var view in views)
                view.SetTime(time);
        }
    }
}
