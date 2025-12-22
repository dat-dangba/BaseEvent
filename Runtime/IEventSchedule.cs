using System;

public interface IEventSchedule
{
    public EventState GetState(bool isAutoReset, EventState currentEventState, DateTime eventStart, DateTime now,
        out DateTime start,
        out DateTime end);
}