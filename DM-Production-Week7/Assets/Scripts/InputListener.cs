using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputListener : MonoBehaviour
{
    public string m_inputName;
    [SerializeField]
    private SimpleEvent m_simpleInputEvent;
    [SerializeField]
    private ToggleEvent m_toggleInputEvent;

    private void Update()
    {
        if (Input.GetButtonDown(m_inputName)) {



            if (m_toggleInputEvent)
            {
                m_toggleInputEvent.ToggleValueAndInvokeEvent();
            }

            if (m_simpleInputEvent)
            {
                m_simpleInputEvent.ActivateEvent();
            }

        }
    }
}
