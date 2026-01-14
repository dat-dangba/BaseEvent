using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEventManager<INSTANCE> : MonoBehaviour
{
    public static INSTANCE Instance { get; private set; }

#if UNITY_EDITOR
    [SerializeReference] private List<GameEvent> events = new();
#endif
    protected readonly Dictionary<string, GameEvent> eventDict = new();

    protected bool isUpdateEvent;

#if UNITY_EDITOR
    protected virtual void Reset()
    {
    }
#endif

    protected virtual void Awake()
    {
        if (Instance == null)
        {
            Instance = GetComponent<INSTANCE>();

            Transform root = transform.root;
            if (root != transform)
            {
                DontDestroyOnLoad(root);
            }
            else
            {
                DontDestroyOnLoad(gameObject);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    protected virtual void OnEnable()
    {
    }

    protected virtual void OnDisable()
    {
    }

    protected virtual void Start()
    {
        AddEvent();
#if UNITY_EDITOR
        ShowEventInspector();
#endif
    }

    private void AddEvent()
    {
        foreach (var item in GetEvents())
        {
            eventDict.Add(item.GetType().ToString(), item);
        }
    }

    private void ShowEventInspector()
    {
        events.Clear();
        foreach (var item in eventDict)
        {
            events.Add(item.Value);
        }
    }

    protected virtual void Update()
    {
        UpdateTimeEvent();
    }

    protected virtual void UpdateTimeEvent()
    {
        if (!isUpdateEvent) return;
        DateTime now = GetDateTimeNow();
        foreach (var e in eventDict)
        {
            e.Value.Update(now);
        }
    }

    protected virtual void FixedUpdate()
    {
    }

    public T GetEvent<T>() where T : GameEvent
    {
        if (eventDict.TryGetValue(nameof(T), out GameEvent value))
        {
            return (T)value;
        }

        return null;
    }

    public GameEvent GetEvent(Type type)
    {
        return eventDict.GetValueOrDefault(type.ToString());
    }

    protected abstract DateTime GetDateTimeNow();

    protected abstract List<GameEvent> GetEvents();

    // protected abstract void AddEvent(Dictionary<string, GameEvent> events);
}