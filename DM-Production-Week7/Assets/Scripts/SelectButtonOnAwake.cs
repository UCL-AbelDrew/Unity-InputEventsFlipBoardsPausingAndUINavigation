using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectButtonOnAwake : MonoBehaviour
{
    public Button m_button;
    private void Awake()
    {
        m_button.Select();
    }
}
