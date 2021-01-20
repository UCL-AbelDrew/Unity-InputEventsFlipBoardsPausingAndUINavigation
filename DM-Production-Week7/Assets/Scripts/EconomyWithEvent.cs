using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public enum EventCondition {LESS,LESSOREQUAL,EQUALS,GREATEROREQUAL,GREATER};
[Serializable]
public class EconomyEvent : UnityEvent<GameObject> { }

public class EconomyWithEvent : MonoBehaviour
{
    public Slider m_economySlider;
    [SerializeField]
    private int m_economyCurrentValue;
    public int m_economyMaxValue;
    public int m_economyMinValue;
    public int m_economyStartValue;
    public EventCondition m_eventCondition;
    public int m_eventValue;
    public EconomyEvent m_economyEvent;
    public float m_normalisedValue 
    { 
        get { 
            return m_normalisedCurrentValue; 
        } 
    }
    [SerializeField]
    private float m_normalisedCurrentValue;
    public bool m_pauseEventsAfterExecution;
    private bool m_pauseEvents;
    private void Start()
    {
        SetCurrentValue(m_economyStartValue);
    }
    public void SetCurrentValue(int value)
    {
        if (value < m_economyMinValue)
        {
            m_economyCurrentValue = m_economyMinValue;
        }
        else if (value > m_economyMaxValue)
        {
            m_economyCurrentValue = m_economyMaxValue;
        }
        else
        {
            m_economyCurrentValue = value;
        }
        UpdateEconomy();
    }
    public void SubtractValue(int value)
    {
        m_economyCurrentValue -= value;
        if(m_economyCurrentValue < m_economyMinValue)
        {
            m_economyCurrentValue = m_economyMinValue;
        }
        UpdateEconomy();
    }
    public void AddValue(int value)
    {
        m_economyCurrentValue += value;
        if (m_economyCurrentValue > m_economyMaxValue)
        {
            m_economyCurrentValue = m_economyMaxValue;
        }

        UpdateEconomy();
    }
    public void ResetValue()
    {
        SetCurrentValue(m_economyStartValue);
    }
    public void PauseEvents(bool value)
    {
        m_pauseEvents = value;
    }
    private void UpdateEconomy()
    {
        UpdateNormalisedEconomyValue();
        UpdateSlider();
        EvaluateEventValue();

    }
    private void UpdateNormalisedEconomyValue()
    {
        if (m_economyMaxValue - m_economyMinValue != 0)
        {
            float val = m_economyCurrentValue;
            float min = m_economyMinValue;
            float max = m_economyMaxValue;
            float value = (val - min) / (max - min);
            m_normalisedCurrentValue = value;
        }
        else
        {
            Debug.Log("avoiding divide by zero");
            m_normalisedCurrentValue = 0;
        }
    }
    private void UpdateSlider()
    {
        if (m_economySlider)
        {
            m_economySlider.value = m_normalisedCurrentValue;
        }
    }
    private void EvaluateEventValue()
    {
        
        switch (m_eventCondition)
        {
            case EventCondition.LESS:
                if(m_economyCurrentValue < m_eventValue)
                {
                    RunEvent();
                }
                break;
            case EventCondition.LESSOREQUAL:
                if (m_economyCurrentValue <= m_eventValue)
                {
                    RunEvent();
                }
                break;
            case EventCondition.EQUALS:
                if (m_economyCurrentValue == m_eventValue)
                {
                    RunEvent();
                }
                break;
            case EventCondition.GREATEROREQUAL:
                if (m_economyCurrentValue >= m_eventValue)
                {
                    RunEvent();
                }
                break;
            case EventCondition.GREATER:
                if (m_economyCurrentValue > m_eventValue)
                {
                    RunEvent();
                }
                break;
        }
    }
    private void RunEvent()
    {
        if (m_pauseEventsAfterExecution)
        {
            if (!m_pauseEvents)
            {
                m_pauseEvents = true;
                m_economyEvent.Invoke(gameObject);
            }
        }
        else
        {
            m_economyEvent.Invoke(gameObject);
        }
    }

}
