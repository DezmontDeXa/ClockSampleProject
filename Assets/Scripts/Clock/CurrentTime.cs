using UnityEngine;
using Zenject;
using System;

namespace DDX.Clock
{
    public class CurrentTime : MonoBehaviour
    {
        private WebTime _webTime;

        private float _lastUpdateUnityTime = 0;
        private TimeSpan _lastUpdateTime = TimeSpan.Zero;

        public TimeSpan Time { get; private set; }

        public event EventHandler<TimeSpan> TimeChanged;

        [Inject]
        private void Init(WebTime webTime)
        {
            _webTime = webTime;
            _webTime.WebTimeUpdated += WebTimeUpdated;
        }

        private void WebTimeUpdated(object sender, TimeSpan e)
        {
            Time = e;
            _lastUpdateTime = e;
            _lastUpdateUnityTime = UnityEngine.Time.realtimeSinceStartup;
        }

        private void Update()
        {
            Time = GetCurrentTime();
            TimeChanged?.Invoke(this, Time);
        }

        private TimeSpan GetCurrentTime()
        {
            return _lastUpdateTime + new TimeSpan(0, 0, (int)(UnityEngine.Time.realtimeSinceStartup - _lastUpdateUnityTime));
        }

    }
}
