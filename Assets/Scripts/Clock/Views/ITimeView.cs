using System;

namespace DDX.Clock.Views
{
    public interface ITimeView
    {
        void SetTime(TimeSpan time);
    }
}