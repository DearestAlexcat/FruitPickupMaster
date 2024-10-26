using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class CustomEvent
{
    [SerializeField]
    string name;
    public string Name
    {
        get
        {
            return name;
        }
        set
        {
            name = value;
        }
    }

    [Space]
    [SerializeField]
    UnityEvent mainEvent;
    public UnityEvent MainEvent
    {
        get
        {
            return mainEvent;
        }
        set
        {
            mainEvent = value;
        }
    }
}

public class AnimationEvent : MonoBehaviour
{
    [SerializeField]
    List<CustomEvent> events;
    public List<CustomEvent> Events
    {
        get
        {
            return events;
        }
        set
        {
            events = value;
        }
    }

    public void AddEvent(CustomEvent thisEvent)
    {
        events.Add(thisEvent);
    }

    public void EnableEvent(int value)
    {
        Events[value].MainEvent.Invoke();
    }

    public void EnableEventWithName(string name)
    {
        for (int i = 0; i < Events.Count; i++)
        {
            if (Events[i].Name == name)
            {
                Events[i].MainEvent.Invoke();

                return;
            }
        }
    }
}
