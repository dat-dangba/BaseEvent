using System;

public class CycleSchedule : BaseSchedule
{
    private readonly DateTime cycleStart;
    private readonly TimeSpan inactiveDuration;

    public CycleSchedule(
        DateTime cycleStart,
        TimeSpan duration,
        TimeSpan inactiveDuration)
    {
        this.cycleStart = cycleStart;
        this.duration = duration;
        this.inactiveDuration = inactiveDuration;
    }

    // public override EventState GetState(bool isAutoReset, EventState currentEventState, DateTime eventStart,
    //     DateTime now,
    //     out DateTime start, out DateTime end)
    // {
    //     var cycleLength = activeDuration + inactiveDuration;
    //
    //     if (now < cycleStart)
    //     {
    //         start = cycleStart;
    //         end = cycleStart + activeDuration;
    //         return EventState.Inactive;
    //     }
    //
    //     var elapsed = now - cycleStart;
    //     var offsetTicks = elapsed.Ticks % cycleLength.Ticks;
    //     var offset = TimeSpan.FromTicks(offsetTicks);
    //
    //     if (offset < activeDuration)
    //     {
    //         // đang trong phase Active
    //         start = now - offset;
    //         end = start + activeDuration;
    //         return EventState.Active;
    //     }
    //
    //     // đang trong phase Inactive
    //     start = now - offset + activeDuration;
    //     end = start + inactiveDuration;
    //     return EventState.Inactive;
    // }

    protected override EventState GetEventState(DateTime now, DateTime start, DateTime end)
    {
        var cycleLength = duration + inactiveDuration;

        if (now < cycleStart)
        {
            return EventState.Inactive;
        }

        var elapsed = now - cycleStart;
        var offsetTicks = elapsed.Ticks % cycleLength.Ticks;
        var offset = TimeSpan.FromTicks(offsetTicks);

        if (offset < duration)
        {
            return EventState.Active;
        }

        return EventState.Ended;
    }

    protected override DateTime GetEventStart(DateTime now)
    {
        var cycleLength = duration + inactiveDuration;

        if (now < cycleStart)
        {
            return cycleStart;
        }

        var elapsed = now - cycleStart;
        var offsetTicks = elapsed.Ticks % cycleLength.Ticks;
        var offset = TimeSpan.FromTicks(offsetTicks);

        if (offset < duration)
        {
            return now - offset;
        }

        return now - offset + duration;
    }
}