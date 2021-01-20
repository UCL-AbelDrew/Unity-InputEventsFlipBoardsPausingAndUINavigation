using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMove : MonoBehaviour
{
    private Vector3 m_direction;
    [SerializeField]
    private GameObject m_targetObject = null;
    [SerializeField]
    private float m_speed = 1f;
    [SerializeField]
    private AnimationCurve m_speedCurve = new AnimationCurve(new Keyframe(0, 1), new Keyframe(1, 1));
    private float m_speedCurveValue;

    private float m_targetDistance;
    private float m_movementSpeed;

    private Vector3 m_startPosition;
    private Vector3 m_currentPosition;
    private Vector3 m_targetPosition;



    private void Start()
    {
        m_startPosition = transform.position;
        m_targetPosition = m_targetObject.transform.position;
    }

    private void FixedUpdate()
    {
        m_currentPosition = transform.position;

        m_movementSpeed = m_speed * Time.deltaTime;

        m_direction = m_targetPosition - m_currentPosition;
        m_direction = m_direction.normalized;
        
        m_targetDistance = Vector3.Distance(m_targetPosition, m_currentPosition);

        CalculateCurve();
        m_movementSpeed *= m_speedCurveValue;


        if (m_movementSpeed > m_targetDistance)
        {
           Move( m_direction * m_targetDistance);
        }
        else
        {
            Move(m_direction * m_movementSpeed);
        }   
            
    }

    private void CalculateCurve()
    {
        float journeyLength = Vector3.Distance(m_startPosition, m_targetPosition);
        float journeyTravelled = journeyLength - m_targetDistance;
        MathsHelpers mathsHelper = new MathsHelpers();
        float normalisedJourneyDistance = mathsHelper.NormaliseValue(0, journeyLength, journeyTravelled);
        m_speedCurveValue = m_speedCurve.Evaluate(normalisedJourneyDistance);
    }

    public void Move(Vector3 direction)
    {
        transform.Translate(direction, Space.World);

 
    }
}
