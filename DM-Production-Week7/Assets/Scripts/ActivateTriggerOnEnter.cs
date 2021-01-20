using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class ActivateTriggerEvent : UnityEvent<GameObject> { }
public class ActivateTriggerOnEnter : MonoBehaviour
{
    public bool m_sendColidingObject;
    public bool m_filterTriggerObjects;
    public int[] m_allowedObjectID;
    public ActivateTriggerEvent m_activateTriggerEvent;
    public void OnTriggerEnter(Collider other)
    {
        if (m_filterTriggerObjects)
        {
            ObjectID otherID = other.gameObject.GetComponent<ObjectID>();
            if (otherID)
            {
                for (int i = 0; i < m_allowedObjectID.Length; i++)
                {

                    if (otherID.m_iD == m_allowedObjectID[i])
                    {
                        Activate(other.gameObject);
                        break;
                    }
                }
            }
        }
        else
        {
            Activate(other.gameObject);
        }
      
    }
    private void Activate(GameObject other)
    {           
        if (m_sendColidingObject)
        {
            m_activateTriggerEvent.Invoke(other);
        }
        else
        {
            m_activateTriggerEvent.Invoke(this.gameObject);
        }

    }

}
