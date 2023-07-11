using System;
using System.Collections;
using UnityEngine;

namespace DDX.Clock.Alarm.Inputs
{
    public class AlarmView : MonoBehaviour, IAlarmView
    {
        [SerializeField] private GameObject Content;

        public void Hide()
        {
            Content.SetActive(false);
        }

        public void Show()
        {
            Content.SetActive(true);
        }
    }
}
