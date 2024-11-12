using System;
using System.Collections.Generic;

public enum EventType
{
    EnemyDead,
    PlayerHPChanged,
    GameOver,
    LevelUp
}

public static class EventBus
{
    private static Dictionary<EventType, Action> eventDictionary = new Dictionary<EventType, Action>();

    public static void Subscribe(EventType eventName, Action listener)
    {
        if (eventDictionary.TryGetValue(eventName, out Action existingEvent))
        {
            eventDictionary[eventName] = existingEvent + listener;
        }
        else
        {
            eventDictionary.Add(eventName, listener);
        }
    }

    public static void Unsubscribe(EventType eventName, Action listener)
    {
        if (eventDictionary.TryGetValue(eventName, out Action existingEvent))
        {
            eventDictionary[eventName] = existingEvent - listener;

            if (eventDictionary[eventName] == null)
            {
                eventDictionary.Remove(eventName);
            }
        }
    }

    public static void Publish(EventType eventName)
    {
        if (eventDictionary.TryGetValue(eventName, out Action thisEvent))
        {
            thisEvent.Invoke();
        }
    }
}

