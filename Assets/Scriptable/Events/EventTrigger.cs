using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameEvent", menuName = "Events/Game Event")]
public class EventTrigger : ScriptableObject
{
    private readonly List<IGameEventListener> m_eventListeners = new List<IGameEventListener>();

    public void Raise()
    {
        for (int i = m_eventListeners.Count - 1; i >= 0; i--)
        {
            m_eventListeners[i].OnEventRaised();
        }
    }
    public void RegisterListener(IGameEventListener listener)
    {
        if (!m_eventListeners.Contains(listener))
            m_eventListeners.Add(listener);
    }
    public void UnregisterListener(IGameEventListener listener)
    {
        if (m_eventListeners.Contains(listener))
            m_eventListeners.Remove(listener);
    }
}