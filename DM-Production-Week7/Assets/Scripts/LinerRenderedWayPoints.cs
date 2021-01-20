using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class LinerRenderedWayPoints : MonoBehaviour
{
    [SerializeField]
    private WayPointSet m_wayPointSet = null;
    [SerializeField]
    private LineRenderer m_lineRenderer = null;

    private void Update()
    {

        if (m_wayPointSet && m_lineRenderer)
        {
            PopulateLineRendererPositionsWithWayPointSet();
        }

    }

    private void PopulateLineRendererPositionsWithWayPointSet()
    {
        List<Vector3> positionsList = new List<Vector3>();
        for (int i = 0; i < m_wayPointSet.m_wayPoints.Length; i++)
        {
            positionsList.Add(m_wayPointSet.m_wayPoints[i].transform.position);
        }
        Vector3[] positionsArray = positionsList.ToArray();
        m_lineRenderer.positionCount = m_wayPointSet.m_wayPoints.Length;
        m_lineRenderer.SetPositions(positionsArray);
    }
}
