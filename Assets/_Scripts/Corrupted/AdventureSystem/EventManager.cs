using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Corrupted;
using UnityEngine.Events;

public class EventManager : Singleton<EventManager>
{

    public static Dictionary<string, UnityEvent> events = new Dictionary<string, UnityEvent>();
    public static System.Action<string> OnEventPlay;
    
    public EventListener[] listeners;




    // Start is called before the first frame update
    void Start()
    {
        foreach(EventListener el in listeners)
        {
            events.Add(el.key, el.action);
        }
    }

    //private void OnDestroy()
    //{
    //    foreach(EventListener el in listeners)
    //    {
    //        events[el.key].RemoveListener(el.action.)
    //    }
    //}


    public static void AddListener(string key, System.Action action)
    {
        if (events.ContainsKey(key))
        {
            events[key].AddListener(() => action());
        }
        else
        {
            Debug.LogError("EventManager: Can not add event because it does not exist!");
        }
    }

    public static void PlayEvent(string key)
    {
        if (events.ContainsKey(key))
        {
            OnEventPlay?.Invoke(key);
            events[key].Invoke();
        }
        else
        {
            Debug.LogError("EventManager: Can not play event because it does not exist!");
        }
    }

    public static void ClearEventListeners(string key)
    {
        if (events.ContainsKey(key))
        {
            events[key].RemoveAllListeners();
        }
        else
        {
            Debug.LogError("EventManager: Can not clear event because it does not exist!");
        }
    }

    public static void ClearAllEventListeners()
    {
        foreach(string s in events.Keys)
        {
            ClearEventListeners(s);
        }
    }

    public static void RemoveEventListener(string key, System.Action action)
    {
        if (events.ContainsKey(key))
        {
            events[key].RemoveListener(() => action());
        }
        else
        {
            Debug.LogError("EventManager: Can not clear event because it does not exist!");
        }
    }

    public static void DebugLog(string log)
    {
        Debug.Log("Sequence: " + log);
    }




}

[System.Serializable]
public struct EventListener
{
    public string key;
    public UnityEvent action;
}
