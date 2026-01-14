using System;
using UnityEngine;

namespace DBD.BaseEvent.Sample
{
    public class TestCycleSchedule : GameEvent
    {
        public TestCycleSchedule()
        {
            //get data
            EventState eventState =
                Enum.Parse<EventState>(PlayerPrefs.GetString("cycle_event_state", EventState.Inactive.ToString()));
            string startTime = PlayerPrefs.GetString("cycle_event_start_time", "");
            Init(eventState, startTime);
        }

        protected override IEventSchedule GetEventSchedule()
        {
            DateTime startDate = new DateTime(2025, 12, 22, 0, 0, 0);
            return new CycleSchedule(startDate, TimeSpan.FromDays(2), TimeSpan.FromDays(1));
        }

        protected override void OnStateChanged(EventState eventState)
        {
            PlayerPrefs.SetString("cycle_event_state", eventState.ToString());
            Debug.Log($"datdb - TestCycleSchedule  OnStateChanged {eventState}");
        }

        protected override void OnNewEventIteration(string eventStartTime)
        {
            PlayerPrefs.SetString("cycle_event_start_time", eventStartTime);
            Debug.Log($"datdb - TestCycleSchedule OnNewEventIteration {eventStartTime}");
        }

        protected override void ResetData()
        {
        }
    }
}