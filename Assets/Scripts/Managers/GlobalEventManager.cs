using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// DO I NEED THIS CLASS? OR SET UP EVENTS MORE LOCALLY
public class GlobalEventManager : MonoBehaviour
{
    public enum EventTypes
    {
      PlayerJoined,
      Player1HealthDecrease,
      Player1HealthIncrease,
      Player2HealthDecrease,
      Player2HealthIncrease,
      BeginCountdown,
      BeginGame,
      GameOver,
    }

    // Main Dictionary of Events
    private Dictionary<EventTypes, UnityEvent<object>> EventDictionary = new();

    // Add a listener to a type of event.
    // If the event doesn't exist yet, it will be created and then the listener added, otherwise the corresponding listener will just be added
    public void AddListener(EventTypes type, UnityAction<object> action)
    {
        if(!EventDictionary.ContainsKey(type))
        {
            EventDictionary.Add(type, new UnityEvent<object>());
        }
        EventDictionary[type].AddListener(action);
    }

    // Remove a listerner from an event
    public void RemoveListener(EventTypes type, UnityAction<object> action)
    {
        if(EventDictionary.ContainsKey(type))
        {
            EventDictionary[type].RemoveListener(action);
        }
    }

    // Dispatch an event (If the event exists), passing data as an argument
    // TODO - How to ensure this data is correctly matching the listener that will recieve it???
    public void Dispatch(EventTypes type, object data)
    {
        if(EventDictionary.ContainsKey(type)) {
            Debug.Log("Event " + Enum.GetName(typeof(EventTypes), type) + " Dispatched");
            EventDictionary[type]?.Invoke(data);
        }
    }
}





