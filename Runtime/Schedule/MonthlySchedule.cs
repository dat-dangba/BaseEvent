using System;
using UnityEngine;

public class MonthlySchedule : BaseSchedule
{
    private readonly int startDay; // 1 → 28 (khuyên không quá 28)

    public MonthlySchedule(int startDay, TimeSpan startAt)
    {
        this.startDay = Mathf.Clamp(startDay, 1, 28);
        this.startAt = startAt;
    }

    protected override DateTime GetEventStart(DateTime now)
    {
        DateTime start = new DateTime(
            now.Year,
            now.Month,
            startDay,
            startAt.Hours,
            startAt.Minutes,
            startAt.Seconds
        );
// nếu chưa tới → lùi về tháng trước
        if (now < start)
        {
            var prevMonth = now.AddMonths(-1);
            start = new DateTime(
                prevMonth.Year,
                prevMonth.Month,
                startDay,
                startAt.Hours,
                startAt.Minutes,
                startAt.Seconds
            );
        }

        int daysInMonth = DateTime.DaysInMonth(start.Year, start.Month);
        duration = TimeSpan.FromDays(daysInMonth);
        return start;
    }

    // public EventState GetState(bool isAutoReset, EventState currentEventState, DateTime eventStart, DateTime now,
    //     out DateTime start, out DateTime end)
    // {
    //     start = new DateTime(
    //         now.Year,
    //         now.Month,
    //         startDay,
    //         startTime.Hours,
    //         startTime.Minutes,
    //         startTime.Seconds
    //     );
    //
    //     // nếu chưa tới → lùi về tháng trước
    //     if (now < start)
    //     {
    //         var prevMonth = now.AddMonths(-1);
    //         start = new DateTime(
    //             prevMonth.Year,
    //             prevMonth.Month,
    //             startDay,
    //             startTime.Hours,
    //             startTime.Minutes,
    //             startTime.Seconds
    //         );
    //     }
    //
    //     end = start + duration;
    //
    //     return GetEventState(isAutoReset, currentEventState, eventStart, now, start, end);
    // }
    //
    // private EventState GetEventState(bool isAutoReset, EventState currentEventState,
    //     DateTime eventStart, DateTime now, DateTime nextEventStart, DateTime nextEventEnd)
    // {
    //     EventState eventState = GetEventState(now, nextEventStart, nextEventEnd);
    //
    //     if (currentEventState == EventState.Reset)
    //     {
    //         return eventState is EventState.Inactive or EventState.Active ? eventState : EventState.Reset;
    //     }
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