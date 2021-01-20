using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObjectToPosition : MonoBehaviour
{
    public Vector3 m_cachedPosition;
    public Quaternion m_cachedRotation;
   public void MoveToCachedPositionAndRotation(GameObject other)
    {
        other.transform.position = m_cachedPosition;
        other.transform.rotation = m_cachedRotation;
    }

    public void SetCachedPositionAndRotation(GameObject other)
    {
        m_cachedPosition = other.gameObject.transform.position;
        m_cachedRotation = other.gameObject.transform.rotation;
    }
}
