using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class TargetPositionEvent : UnityEvent<GameObject>{ }
public class EventWhenObjectReachesPosition : MonoBehaviour
{
    public GameObject m_eventPositionObject;
    public TargetPositionEvent m_targetPositionEvent;

    private bool m_positionChanged;

    private void FixedUpdate()
    {

        // If at position, invoke once.
        if (transform.position == m_eventPositionObject.transform.position)
        {
            if (m_positionChanged)
            {
                m_targetPositionEvent.Invoke(gameObject);
                m_positionChanged = false;
            }
            
        }
        // position not reached or changed, allow event to be resent if position reached again
        else
        {
            m_positionChanged = true;

        }
    }


}
