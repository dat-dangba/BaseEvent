using System;

public abstract class BaseSchedule : IEventSchedule
{
    protected TimeSpan startAt;
    protected TimeSpan duration;

    private EventState GetEventState(bool isAutoReset, EventState currentEventState,
        DateTime eventStart, DateTime now, DateTime start, DateTime end)
    {
        EventState eventState = GetEventState(now, start, end);

        if (currentEventState == EventState.Reset)
        {
            return eventState is EventState.Inactive or EventState.Active ? eventState : EventState.Reset;
        }

        if (isAutoReset) return eventState;

        if (currentEventState == EventState.Ended) return EventState.Ended;

        if (currentEventState == EventState.Active && now > eventStart + duration) return EventState.Ended;

        return eventState;
    }

    protected virtual EventState GetEventState(DateTime now, DateTime start, DateTime end)
    {
        if (now < start) return EventState.Inactive;
        if (now <= end) return EventState.Active;
        return EventState.Ended;
    }

    public virtual EventState GetState(bool isAutoReset, EventState currentEventState,
        DateTime eventStart, DateTime now,
        out DateTime start, out DateTime end)
    {
        start = GetEventStart(now);
        end = start + duration;
        return GetEventState(isAutoReset, currentEventState, eventStart, now, start, end);
    }

    protected abstract DateTime GetEventStart(DateTime now);
}