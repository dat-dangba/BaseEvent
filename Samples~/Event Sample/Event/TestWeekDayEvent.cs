using System;
using UnityEngine;

namespace EventSample
{
    public class TestWeekDayEvent : GameEvent
    {
        public TestWeekDayEvent()
        {
            Init(Enum.Parse<EventState>(PlayerPrefs.GetString("weekday_event_state", EventState.Inactive.ToString())),
                PlayerPrefs.GetString("weekday_event_start_time", ""));
        }

        protected override IEventSchedule GetEventSchedule()
        {
            return new WeekdaySchedule(
                new[] { DayOfWeek.Monday, DayOfWeek.Wednesday },
                TimeSpan.Zero,
                TimeSpan.FromHours(24)
            );
        }

        protected override void OnStateChanged(EventState eventState)
        {
            PlayerPrefs.SetString("weekday_event_state", eventState.ToString());
            Debug.Log($"datdb - TestWeekDayEvent  OnStateChanged {eventState}");
        }

        protected override void OnNewEventIteration(string eventStartTime)
        {
            PlayerPrefs.SetString("weekday_event_start_time", eventStartTime);
            Debug.Log($"datdb - TestWeekDayEvent OnNewEventIteration {eventStartTime}");
        }

        protected override void ResetData()
        {
        }
    }
}