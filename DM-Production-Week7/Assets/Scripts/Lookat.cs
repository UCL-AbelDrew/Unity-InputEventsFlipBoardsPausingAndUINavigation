using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lookat : MonoBehaviour
{
    public GameObject m_target;
  
    private void Update()
    {
        transform.LookAt(m_target.transform.position, Vector3.up);
    }
    
}
