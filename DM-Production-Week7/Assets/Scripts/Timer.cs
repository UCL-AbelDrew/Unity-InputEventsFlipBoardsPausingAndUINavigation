using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public float m_currentTime;
    public float m_eventTime;
    
    private bool m_timerRunning = false;
    public bool m_resetAfterEvent = true;
    public bool m_stopAfterEvent = true;
    public SimpleEvent m_simpleEvent;

    private void FixedUpdate()
    {
        if (m_timerRunning)
        {
            m_currentTime += Time.deltaTime;

            if (m_currentTime >= m_eventTime)
            {
                if (m_simpleEvent)
                {
                    m_simpleEvent.ActivateEvent(gameObject);

                }
                if (m_resetAfterEvent)
                {
                    Reset();
                }
                if (m_stopAfterEvent)
                {
                    PauseTimer(true);
                }
            }
        }
    }

    public void Reset()
    {
        m_currentTime = 0;
    }

    public void StartTimer()
    {
        m_timerRunning = true;
    }

    public void PauseTimer(bool pause)
    {
        m_timerRunning = !pause;
    }

}
