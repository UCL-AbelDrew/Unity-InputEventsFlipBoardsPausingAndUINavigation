using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EventType { REDUCE, SET, INCREASE, RESET };
public class ModifyEconomy : MonoBehaviour
{
    public EventType m_eventType;
    public int m_value;
    public void Modify(GameObject other)
    {
        EconomyWithEvent economyWithEvent = other.GetComponent<EconomyWithEvent>();
       
        if (economyWithEvent)
        {
            switch (m_eventType)
            {
                case EventType.REDUCE:
                    economyWithEvent.SubtractValue(m_value);
                    break;
                case EventType.SET:
                    economyWithEvent.SetCurrentValue(m_value);
                    break;
                case EventType.INCREASE:
                    economyWithEvent.AddValue(m_value);
                    break;
                case EventType.RESET:
                    economyWithEvent.ResetValue();
                    break;

            }
        }
    }
}
