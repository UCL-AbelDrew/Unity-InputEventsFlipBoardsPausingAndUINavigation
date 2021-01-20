using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastReceiver : MonoBehaviour
{
    public SimpleEvent m_beginRaycastEvent;
    public SimpleEvent m_endRaycastEvent;
   public void OnReceiveRaycast()
    {
        if (m_beginRaycastEvent)
        {
            m_beginRaycastEvent.ActivateEvent();
        }
    }

    public void OnEndRaycast() {

        if (m_endRaycastEvent)
        {
            m_endRaycastEvent.ActivateEvent();
        }
    }


}
