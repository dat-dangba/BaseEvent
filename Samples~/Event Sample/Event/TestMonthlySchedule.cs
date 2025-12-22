using System;
using UnityEngine;

namespace EventSample
{
    public class TestMonthlySchedule : GameEvent
    {
        public TestMonthlySchedule()
        {
            //get data
            Init(Enum.Parse<EventState>(PlayerPrefs.GetString("monthly_event_state", EventState.Inactive.ToString())),
                PlayerPrefs.GetString("monthly_event_start_time", ""));
        }

        protected override IEventSchedule GetEventSchedule()
        {
            return new MonthlySchedule(1, TimeSpan.Zero);
        }

        protected override void OnStateChanged(EventState eventState)
        {
            PlayerPrefs.SetString("monthly_event_state", eventState.ToString());
            Debug.Log($"datdb - TestMonthlySchedule  OnStateChanged {eventState}");
        }

        protected override void OnNewEventIteration(string eventStartTime)
        {
            PlayerPrefs.SetString("monthly_event_start_time", eventStartTime);
            Debug.Log($"datdb - TestMonthlySchedule OnNewEventIteration {eventStartTime}");
        }

        protected override void ResetData()
        {
        }
    }
}