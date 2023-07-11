using Cysharp.Threading.Tasks;
using System;

namespace DDX.Clock.TimeProviders
{
    public interface ITimeProvider
    {
        UniTask<TimeSpan> GetTimeAsync();
    }
}