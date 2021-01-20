using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicRaycast : MonoBehaviour
{
    public LayerMask m_layerMask;
    public float m_rayLength;

    void FixedUpdate()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, m_rayLength, m_layerMask))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            Debug.Log("Did Hit");
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * m_rayLength, Color.white);
            Debug.Log("Did not Hit");
        }
    }


  
}
