using System;
using UnityEngine;

namespace DBD.BaseEvent.Sample
{
    public class TestDailySchedule : GameEvent
    {
        public TestDailySchedule()
        {
            //get data
            EventState eventState =
                Enum.Parse<EventState>(PlayerPrefs.GetString("daily_event_state", EventState.Inactive.ToString()));
            string startTime = PlayerPrefs.GetString("daily_event_start_time", "");
            Init(eventState, startTime);
        }

        protected override IEventSchedule GetEventSchedule()
        {
            return new DailySchedule(TimeSpan.FromHours(0), TimeSpan.FromHours(24));
        }

        protected override void OnStateChanged(EventState eventState)
        {
            PlayerPrefs.SetString("daily_event_state", eventState.ToString());
            Debug.Log($"datdb - TestDailySchedule  OnStateChanged {eventState}");
        }

        protected override void OnNewEventIteration(string eventStartTime)
        {
            PlayerPrefs.SetString("daily_event_start_time", eventStartTime);
            Debug.Log($"datdb - TestDailySchedule OnNewEventIteration {eventStartTime}");
        }

        protected override void ResetData()
        {
            Debug.Log($"datdb - TestDailySchedule ResetData");
        }
    }
}