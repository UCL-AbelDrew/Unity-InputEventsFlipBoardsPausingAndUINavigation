using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAround : MonoBehaviour
{
    public SimpleEvent m_eventAfterRotation;
    public float m_rotationSpeed = 360;
    public float m_rotationDegrees = 180;
    private float m_rotated;
    [SerializeField]
    private GameObject m_target;

    private bool m_flip;
    private bool m_directionFlip;
    private float m_directionSpeed;
    public void Flip()
    {
        // reverse each time we run the method.
        m_directionFlip = !m_directionFlip;

        m_directionSpeed = m_rotationSpeed;
        if (m_directionFlip)
        {
            m_directionSpeed = -m_directionSpeed;
        }
        m_rotated = 0;
        m_flip = true;
    }

    private void Update()
    {        
        if (m_flip)
        {
           
            m_rotated += m_rotationSpeed * Time.deltaTime;

            // if rotated would be too far then we rotate the small amount and end it.
            if (m_rotated >= m_rotationDegrees)
            {
                // get the value of the rotation from the previous frame
                float finalRot = m_rotated - (m_rotationSpeed * Time.deltaTime);
                float finalSpeed = m_rotationDegrees - finalRot;
                // if flipped reverse speed
                if (m_directionFlip)
                {
                    finalSpeed = -finalSpeed;
                }
                // rotate by the total required degrees minus the degrees rotated so far.
                transform.RotateAround(m_target.transform.position, m_target.transform.forward, finalSpeed);
                m_flip = false;
                // call the event
                if (m_eventAfterRotation)
                {
                    m_eventAfterRotation.ActivateEvent();
                }
                return;
            }
            else
            {
                transform.RotateAround(m_target.transform.position, m_target.transform.forward, m_directionSpeed * Time.deltaTime);
            }
       
        }
    }
}
