using System;

public class DailySchedule : BaseSchedule
{
    // private readonly TimeSpan startTime;
    // private readonly TimeSpan duration;

    public DailySchedule(TimeSpan startAt, TimeSpan duration)
    {
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
    //     start = now.Date + startTime;
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

    // private EventState GetEventState(DateTime now, DateTime start, DateTime end)
    // {
    //     if (now < start) return EventState.Inactive;
    //     if (now <= end) return EventState.Active;
    //     return EventState.Ended;
    // }
}