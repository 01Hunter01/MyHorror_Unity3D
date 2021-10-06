using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAction<T> : ScriptableObject
{
    private List<IGameActionListener<T>> Listeners = new List<IGameActionListener<T>>();
    public void InvokeAction(T m_num)
    {
        for (int i = Listeners.Count - 1; i >= 0; i--)
        {
            Listeners[i].OnEventRaized(m_num);
        }
    }
    public void RegisterListener(IGameActionListener<T> listeners)
    {
        if (!Listeners.Contains(listeners))
        {
            Listeners.Add(listeners); 
        }
        else
        {
            Debug.Log("listeners already contains listener: " + listeners);
        }
    }
    public void UnRegisterListener(IGameActionListener<T> listeners)
    {
        if (Listeners.Contains(listeners))
        {
            Listeners.Remove(listeners);
        }
    }
}
