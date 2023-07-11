using DDX.Clock.TimeProviders;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using System.Linq;
using System;
using Cysharp.Threading.Tasks;
using System.Threading;

namespace DDX.Clock
{
    public class WebTime : IDisposable
    {
        private TimeSpan _fixRate = new TimeSpan(1, 0, 0);
        private List<ITimeProvider> _timeProviders;
        private TimeSpan _lastWebTime;
        private CancellationTokenSource _cts = new CancellationTokenSource();

        public TimeSpan LastWebTime => _lastWebTime;

        public event EventHandler<TimeSpan> WebTimeUpdated;


        [Inject]
        public void Init(IEnumerable<ITimeProvider> timeProviders)
        {
            _timeProviders = timeProviders.ToList();
            UpdateCurrentTimeAsync();
            UniTask.RunOnThreadPool(FixTime);
        }

        private async void FixTime()
        {
            while (!_cts.IsCancellationRequested)
            {
                await UniTask.Delay(_fixRate);
                await UpdateCurrentTimeAsync();
            }
        }

        private async UniTask UpdateCurrentTimeAsync()
        {
            foreach (var provider in _timeProviders.ToList())
            {
                try
                {
                    _lastWebTime = await provider.GetTimeAsync();
                    WebTimeUpdated.Invoke(this, _lastWebTime);
                    return;
                }
                catch (Exception ex)
                {
                    _timeProviders.Remove(provider);
                    if (_timeProviders.Count > 0)
                        Debug.Log(ex.Message);
                    else
                        Debug.LogError("All providers not available");
                    continue;
                }
            }
        }

        public void Dispose()
        {
            _cts.Cancel();
        }

    }
}
