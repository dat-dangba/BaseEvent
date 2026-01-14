using System;
using UnityEngine;

namespace DBD.BaseEvent.Sample
{
    public class TestWeeklyEvent : GameEvent
    {
        public TestWeeklyEvent()
        {
            Init(Enum.Parse<EventState>(PlayerPrefs.GetString("weekly_event_state", EventState.Inactive.ToString())),
                PlayerPrefs.GetString("weekly_event_start_time", ""));
        }

        protected override IEventSchedule GetEventSchedule()
        {
            return new WeeklySchedule(DayOfWeek.Monday, TimeSpan.Zero, TimeSpan.FromDays(7));
        }

        protected override void OnStateChanged(EventState eventState)
        {
            PlayerPrefs.SetString("weekly_event_state", eventState.ToString());
            Debug.Log($"datdb - TestWeeklyEvent  OnStateChanged {eventState}");
        }

        protected override void OnNewEventIteration(string eventStartTime)
        {
            PlayerPrefs.SetString("weekly_event_start_time", eventStartTime);
            Debug.Log($"datdb - TestWeeklyEvent OnNewEventIteration {eventStartTime}");
        }

        protected override void ResetData()
        {
        }
    }
}