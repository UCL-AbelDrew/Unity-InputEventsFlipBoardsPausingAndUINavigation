using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MoveType { PLAYONCE, LOOP, PINGPONG};
public class MoveObjectOverTime : MonoBehaviour
{
    [SerializeField]
    private MoveType m_moveType = MoveType.PLAYONCE;
    [SerializeField]
    private WayPointSet m_wayPointSet = null;
    public float m_maxSpeed;
    private bool m_moving;
    [SerializeField]
    private GameObject m_startObject;
    [SerializeField]
    private GameObject m_targetObject;
    private Vector3 m_targetDirection;
    [SerializeField]
    private float m_targetDistance;

    private int m_targetIndex = 0;
    private bool m_playReverse;

    [SerializeField]
    private float m_epsilon = 0.001f;


    private void Start()
    {
        if (m_wayPointSet)
        {
            m_startObject = m_wayPointSet.m_wayPoints[0];
            transform.position = m_startObject.transform.position;
            m_targetObject = m_wayPointSet.m_wayPoints[1];
            m_moving = true;
        }
        else
        {
            Debug.LogError("No WayPoint Set on Moving Object");
        }
  
        
    }

    void FixedUpdate()
    {
        if (m_moving)
        {

            Move();


            if (m_targetDistance <= m_epsilon)
            {
                // change targets depending on move type once they're "close enough" to the next target.                
                switch (m_moveType)
                {
                    case MoveType.PLAYONCE:
                        m_targetIndex++;

                        if (m_targetIndex < m_wayPointSet.m_wayPoints.Length)
                        {
                            m_targetObject = m_wayPointSet.m_wayPoints[m_targetIndex];
                        }
                        else
                        {
                            m_moving = false;
                        }

                        break;
                    case MoveType.LOOP:
                        m_targetIndex++;

                        if (m_targetIndex == m_wayPointSet.m_wayPoints.Length)
                        {
                            m_targetIndex = 0;
                           
                        }
                        m_targetObject = m_wayPointSet.m_wayPoints[m_targetIndex];


                        break;
                    case MoveType.PINGPONG:
                       
                        if (m_playReverse) {
                            m_targetIndex--;
                            if (m_targetIndex == 0)
                            {
                                m_playReverse = false;
                            }
                        }
                        else
                        {
                            m_targetIndex++;
                            if (m_targetIndex == m_wayPointSet.m_wayPoints.Length -1)
                            {
                                m_playReverse = true;
                            }

                        }

                        m_targetObject = m_wayPointSet.m_wayPoints[m_targetIndex];

                        break;

                   
                }
            }          
        }
    }

    private void Move()
    {        
        m_targetDirection = m_targetObject.transform.position - gameObject.transform.position;
        m_targetDistance = Vector3.Distance(gameObject.transform.position, m_targetObject.transform.position);
        Vector3 directionAndSpeed = new Vector3();

        if (m_maxSpeed * Time.deltaTime > m_targetDistance)
        {
            directionAndSpeed = (m_targetDirection.normalized * m_targetDistance);
        }
        else
        {
            directionAndSpeed = m_targetDirection.normalized * (m_maxSpeed * Time.deltaTime);
        }
        transform.Translate(directionAndSpeed, Space.World);
               
    }


}
