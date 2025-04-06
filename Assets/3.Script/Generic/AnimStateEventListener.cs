using System;
using UnityEngine;

public class AnimStateEventListener : MonoBehaviour
{
    public event Action<string,string> OnCallEvent;
    
    public void CallEvent(string eventName, string parameter)
    {
        OnCallEvent?.Invoke(eventName, parameter);
    }
    
}
