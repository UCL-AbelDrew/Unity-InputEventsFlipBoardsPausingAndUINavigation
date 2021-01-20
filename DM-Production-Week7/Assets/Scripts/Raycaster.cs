using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycaster : MonoBehaviour
{
    public LayerMask m_layerMask;
    public float m_rayLength;
    public SimpleEvent m_enterEvent;
    public SimpleEvent m_exitEvent;

    private RaycastReceiver m_raycastReceiver;

    [SerializeField]
    private GameObject m_lastHitObject;
    private bool m_sentExit;

    private void FixedUpdate()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, m_rayLength, m_layerMask))
        {
            m_sentExit = false;
          
            // If there's no object, then set the last object to ourself, this also stops registering an event on a self raycast.
            if (!m_lastHitObject)
            {
                m_lastHitObject = gameObject;
            }
            // Found a new object, Send exit event, update to new object, send enterEvent.
            if (m_lastHitObject != hit.collider.gameObject)
            {
               
                // clear the receiver
                m_raycastReceiver = null;
                // set it to the previous game objects receiver
                m_raycastReceiver = m_lastHitObject.GetComponent<RaycastReceiver>();
                
                if (m_raycastReceiver)
                {
                    m_raycastReceiver.OnEndRaycast();
                    
                }
                if (m_exitEvent)
                {
                    m_exitEvent.ActivateEvent(gameObject);
                }

                m_lastHitObject = hit.collider.gameObject;

                // clear the receiver again
                m_raycastReceiver = null;
                // set it to the new game object receiver
                m_raycastReceiver = m_lastHitObject.GetComponent<RaycastReceiver>();
                
                if (m_raycastReceiver)
                {
                    m_raycastReceiver.OnReceiveRaycast();
                }
                if (m_enterEvent)
                {
                    m_enterEvent.ActivateEvent(gameObject);
                }
            }
            
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            
        }
        else
        {
            // if we have no hit we may have exited the previous object
            // but we only want to send "exit" once.
            if (!m_sentExit)
            {
                if (!m_lastHitObject)
                {
                    m_lastHitObject = gameObject;
                }
                // clear the receiver again
                m_raycastReceiver = null;
                // set it to the new game object receiver
                m_raycastReceiver = m_lastHitObject.GetComponent<RaycastReceiver>();

                if (m_raycastReceiver)
                {
                    m_raycastReceiver.OnEndRaycast();
                }
                if (m_exitEvent)
                {
                    m_exitEvent.ActivateEvent(gameObject);
                }
                m_lastHitObject = null;
                m_sentExit = true;
            }
            
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * m_rayLength, Color.white);          
        }

    }

}
