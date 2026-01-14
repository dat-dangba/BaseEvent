using System;
using System.Collections.Generic;

namespace DBD.BaseEvent
{
    public class WeekdaySchedule : BaseSchedule
    {
        private readonly HashSet<DayOfWeek> validDays;

        public WeekdaySchedule(IEnumerable<DayOfWeek> days, TimeSpan startAt, TimeSpan duration)
        {
            validDays = new HashSet<DayOfWeek>(days);
            this.startAt = startAt;
            this.duration = duration;
        }

        protected override DateTime GetEventStart(DateTime now)
        {
            return now.Date + startAt;
        }

        // public EventState GetState(bool isAutoReset, EventState currentEventState, DateTime eventStart, DateTime now,
        //     out DateTime start, out DateTime end)
        // {
        //     start = now.Date;
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

        protected override EventState GetEventState(DateTime now, DateTime start, DateTime end)
        {
            if (!validDays.Contains(now.DayOfWeek))
                return EventState.Inactive;

            if (now <= end) return EventState.Active;
            return EventState.Ended;
        }
    }
}