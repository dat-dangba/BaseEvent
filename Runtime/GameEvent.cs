using System;
using UnityEngine;

[Serializable]
public abstract class GameEvent
{
#if UNITY_EDITOR
    [SerializeField] private string eventId;
#endif
    [SerializeField] private bool isEventAutoReset;
    [SerializeField] private EventState eventState;
    [SerializeField] private string eventStartTime;

    private IEventSchedule eventSchedule;
    private DateTime eventStartDateTime;

    protected void Init(EventState state, string startTime, bool isAutoReset = false)
    {
#if UNITY_EDITOR
        eventId = GetType().ToString();
#endif
        isEventAutoReset = isAutoReset;
        eventState = state;
        eventStartTime = string.IsNullOrEmpty(startTime) ? "1970-01-01T00:00:00.0000000" : startTime;
        eventSchedule = GetEventSchedule();
        eventStartDateTime = GetStartDateTimeEvent();
    }

    public virtual void Update(DateTime now)
    {
        var newState = eventSchedule.GetState(isEventAutoReset, eventState, eventStartDateTime, now,
            out var start, out var end);
        if (newState != eventState)
        {
            eventState = newState;
            OnStateChanged(eventState);
        }

        if (eventState != EventState.Active || start == eventStartDateTime) return;

        eventStartDateTime = start;
        eventStartTime = GetStartTime();
        ResetData();
        OnNewEventIteration(eventStartTime);
    }

    public virtual void ResetEvent()
    {
        if (eventState != EventState.Ended) return;

        eventState = EventState.Reset;
        OnStateChanged(eventState);
        ResetData();
    }

    private DateTime GetStartDateTimeEvent()
    {
        return DateTime.Parse(eventStartTime, null, System.Globalization.DateTimeStyles.RoundtripKind);
    }

    private string GetStartTime()
    {
        return eventStartDateTime.ToString("o");
    }

    public virtual EventState GetEventState()
    {
        return eventState;
    }

    #region abstract

    protected abstract IEventSchedule GetEventSchedule();

    protected abstract void OnStateChanged(EventState eventState);

    /// <summary>
    /// Gọi khi bước sang đợt event mới
    /// </summary>
    protected abstract void OnNewEventIteration(string eventStartTime);

    /// <summary>
    /// Gọi khi bước sang đợt event mới hoặc khi nhận thưởng khi kết thúc sự kiện mà chưa sang đợt event mới
    /// </summary>
    protected abstract void ResetData();

    #endregion
}