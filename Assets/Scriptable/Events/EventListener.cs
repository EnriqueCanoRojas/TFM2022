using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

[Serializable]
public class EventListener : IGameEventListener
{
    [SerializeField]
    private EventTrigger @event;

    private Action m_OnResponse;

    public void OnEnable(Action response)
    {
        if (@event != null) @event.RegisterListener(this);
        m_OnResponse = response;
    }
    public void OnDisable()
    {
        if (@event != null) @event.UnregisterListener(this);
        @event.UnregisterListener(this);
    }
    public void OnEventRaised()
    {
        m_OnResponse?.Invoke();
    }
}