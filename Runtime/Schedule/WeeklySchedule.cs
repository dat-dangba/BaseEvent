using System;

namespace DBD.BaseEvent
{
    public class WeeklySchedule : BaseSchedule
    {
        private readonly DayOfWeek startDay;

        public WeeklySchedule(DayOfWeek startDay, TimeSpan startAt, TimeSpan duration)
        {
            this.startDay = startDay;
            this.startAt = startAt;
            this.duration = duration;
        }

        protected override DateTime GetEventStart(DateTime now)
        {
            int diff = (7 + (now.DayOfWeek - startDay)) % 7;
            return now.Date.AddDays(-diff) + startAt;
        }

        // public EventState GetState(bool isAutoReset, EventState currentEventState, DateTime eventStart, DateTime now,
        //     out DateTime start, out DateTime end)
        // {
        //     int diff = (7 + (now.DayOfWeek - startDay)) % 7;
        //     start = now.Date.AddDays(-diff);
        //     end = start + duration;
        //
        //     EventState eventState = GetEventState(now, start, end);
        //
        //     if (isAutoReset) return eventState;
        //
        //     if (currentEventState == EventState.Ended) return EventState.Ended;
        //
        //     if (currentEventState == EventState.Active && now > eventStart + duration) return EventState.Ended;
        //
        //     return eventState;
        // }
        //
        // private EventState GetEventState(DateTime now, DateTime nextEventStart, DateTime nextEventEnd)
        // {
        //     if (now < nextEventStart) return EventState.Inactive;
        //     if (now <= nextEventEnd) return EventState.Active;
        //     return EventState.Ended;
        // }
    }
}