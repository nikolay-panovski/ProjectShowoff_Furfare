using System;
using System.Collections.Generic;
using UnityEngine;

public class EventQueue : MonoBehaviour
{
    public delegate void EventHandler(EventData eventData);

    private Dictionary<EventType, EventHandler> subscriberDict = new Dictionary<EventType, EventHandler>();
    private List<EventData> eventList = new List<EventData>();

    #region SINGLETON + DONTDESTROYONLOAD
    private static EventQueue instance = null;

    public static EventQueue Instance
    {
        get
        {
            return instance;
        }
    }

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }
    #endregion

    public void Update()
    {
        PublishEvents();
    }

    public void Subscribe(EventType eventType, EventHandler eventHandler)
    {
        // ~~why the inner 2 lines, and not just Add(type, handler) in the if?
        // because multiple handlers could want messages from one EventType entry and therefore subscribe.
        // also, an event best be discarded/not fired on Invoke if there are no subscribers (handler is null).
        if (!subscriberDict.ContainsKey(eventType))
        {
            EventHandler _handler = null;
            subscriberDict.Add(eventType, _handler);
            //Debug.Log(eventType);
        }
        subscriberDict[eventType] += eventHandler;
    }

    public void Unsubscribe(EventType eventType, EventHandler eventHandler)
    {
        if (subscriberDict.ContainsKey(eventType))
        {
            subscriberDict[eventType] -= eventHandler;
        }
        else
        {
            string warningMsg = "Warning: Event type " + eventType.ToString() +
                                     " doesn't exist in the event manager's subscriber dictionary";
            Debug.Log(warningMsg);
        }
    }

    public void AddEvent(EventData eventData)
    {
        if (!Enum.IsDefined(typeof(EventType), eventData.eventType))
        {
            throw new ArgumentOutOfRangeException("eventData.eventType", "EventType is not defined in the enum.");
        }

        eventList.Add(eventData);
    }

    public void PublishEvents()
    {
        for (int i = eventList.Count - 1; i >= 0; i--)
        {
            EventData data = eventList[i];
            Debug.Log(data);
            if (subscriberDict.ContainsKey(data.eventType))
            {
                subscriberDict[data.eventType]?.Invoke(data);
            }

            else
            {
                string warningMsg = "Warning: Event type " + data.eventType.ToString() +
                                     " doesn't exist in the event manager's subscriber dictionary (there is a publisher but no subscribers)";
                //Console.WriteLine(warningMsg);
                Debug.Log(warningMsg);
            }

            eventList.Remove(data);
        }
    }
}