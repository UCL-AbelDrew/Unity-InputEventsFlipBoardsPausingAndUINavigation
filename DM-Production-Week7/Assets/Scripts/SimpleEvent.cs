using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class GameObjectEvent : UnityEvent<GameObject>{}



public class SimpleEvent : MonoBehaviour
{
    public GameObject m_overrideObjectToSend;
    [SerializeField]
    private GameObjectEvent m_gameObjectEvent = null;
    public void ActivateEvent(GameObject other = null) {
        if (!other)
        {
            other = gameObject;
        }

        if (m_overrideObjectToSend)
        {
            other = m_overrideObjectToSend;
        }

            m_gameObjectEvent.Invoke(other);
    }

}
