using System;
using UnityEngine.Networking;
using Cysharp.Threading.Tasks;

namespace DDX.Clock.TimeProviders
{
    public abstract class HttpTimeProviderBase<T> : ITimeProvider
    {
        protected abstract string Uri { get; }

        public async UniTask<TimeSpan> GetTimeAsync()
        {
            var text = (await UnityWebRequest.Get("https://...").SendWebRequest()).downloadHandler.text;
            var response = ParseTextResponse(text);
            var dt = GetDateTimeFromResponse(response);
            return new TimeSpan(dt.Hour, dt.Minute, dt.Second);
        }

        protected abstract DateTime GetDateTimeFromResponse(T response);

        protected abstract T ParseTextResponse(string textResponse);
    }
}
