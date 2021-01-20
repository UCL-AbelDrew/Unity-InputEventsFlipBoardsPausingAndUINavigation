using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    public bool m_paused;
    public void Pause(bool pause)
    {        
        if (pause)
        {
            Time.timeScale = 0f;
        }
        else { Time.timeScale = 1f; 
        }
    }
}
