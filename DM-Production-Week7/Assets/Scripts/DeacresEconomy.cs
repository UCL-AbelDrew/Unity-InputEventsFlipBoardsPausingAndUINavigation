using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeacresEconomy : MonoBehaviour
{
    public EconomyWithEvent m_economy;
    public int m_decreaseValue;

    private void FixedUpdate()
    {
        m_economy.SubtractValue(m_decreaseValue);
    }
}
