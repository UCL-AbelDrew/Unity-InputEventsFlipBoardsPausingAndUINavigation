using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class OnClick : UnityEvent<GameObject> { }



public class MouseDownEvent : MonoBehaviour
{
    [SerializeField]
    private OnClick m_onClickEvent = null;

    public GameObject m_player;
    public float m_clickDistance = 2f;


    private void OnMouseDown()
    {
        float playerDistance = Vector3.Distance(gameObject.transform.position, m_player.transform.position);

        if (playerDistance < m_clickDistance)
        {
            m_onClickEvent.Invoke(gameObject);
        }
    }
}
