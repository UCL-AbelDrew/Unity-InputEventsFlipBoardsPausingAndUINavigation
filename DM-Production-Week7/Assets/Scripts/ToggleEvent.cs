using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class BoolEvent : UnityEvent<bool> { }


public class ToggleEvent : MonoBehaviour
{
    public bool m_nextValueToSend = true;
    [SerializeField]
    private BoolEvent m_event;

    public void ToggleValueAndInvokeEvent()
    {
        
        m_event.Invoke(m_nextValueToSend);
        m_nextValueToSend = !m_nextValueToSend;
    }
}
