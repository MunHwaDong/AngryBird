using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class EventBus
{
    private static readonly IDictionary<EventType, UnityEvent> events = new Dictionary<EventType, UnityEvent>();

    public static void RegisterEvent(EventType eventType, UnityAction action)
    {
        UnityEvent thisEvent;

        if (events.TryGetValue(eventType, out thisEvent))
        {
            thisEvent.AddListener(action);
        }
        else
        {
            thisEvent = new UnityEvent();
            events.Add(eventType, thisEvent);
            thisEvent.AddListener(action);
        }
    }

    public static void UnregisterEvent(EventType eventType, UnityAction action)
    {
        UnityEvent thisEvent;

        if (events.TryGetValue(eventType, out thisEvent))
        {
            thisEvent.RemoveListener(action);
        }
    }

    public static void Publish(EventType eventType)
    {
        UnityEvent thisEvent;

        if (events.TryGetValue(eventType, out thisEvent))
        {
            thisEvent?.Invoke();
        }
    }
}
