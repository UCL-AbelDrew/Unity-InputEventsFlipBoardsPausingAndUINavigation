using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveObjectSets : MonoBehaviour
{
    public GameObject[] m_firstSet;
    public GameObject[] m_secondSet;

    public void ActivateAlternateObjectSets(bool setTwoActive)
    {       
        for (int i = 0; i < m_firstSet.Length; i++)
            {
            m_firstSet[i].SetActive(!setTwoActive);
            }
            for (int j = 0; j < m_secondSet.Length; j++)
            {
            m_secondSet[j].SetActive(setTwoActive);
            }        
    }

}
