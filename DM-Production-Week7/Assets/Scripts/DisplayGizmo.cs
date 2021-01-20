using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GizmoType { SPHERE, CUBE, RAY};
public class DisplayGizmo : MonoBehaviour
{
    [SerializeField]
    private GizmoType m_gizmoType = GizmoType.SPHERE;
    [SerializeField]
    private bool m_onlyDrawWhenSelected = false;
    [SerializeField]
    private Color m_gizmoColour = new Color(0,0,1,0.5f);
    [SerializeField]
    private float m_gizmoScale = .2f;


    private void OnDrawGizmos()
    {
        Gizmos.color = m_gizmoColour;
        if (!m_onlyDrawWhenSelected)
        {
            
            switch (m_gizmoType)
            {
                case GizmoType.SPHERE:
                    DrawSphere();
                    break;
                case GizmoType.CUBE:
                    DrawCube();
                    break;
                case GizmoType.RAY:
                    DrawRay();
                    break;
            }
        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = m_gizmoColour;
            switch (m_gizmoType)
            {
                case GizmoType.SPHERE:
                    DrawSphere();
                    break;
                case GizmoType.CUBE:
                    DrawCube();
                    break;
            case GizmoType.RAY:
                DrawRay();
                break;
        }

    }

    private void DrawSphere()
    {
        Gizmos.DrawSphere(transform.position, m_gizmoScale);
    }
    private void DrawCube()
    {
        Gizmos.DrawCube(transform.position, new Vector3(m_gizmoScale, m_gizmoScale, m_gizmoScale));
    }

    private void DrawRay()
    {
        Vector3 direction = transform.TransformDirection(Vector3.forward);
        direction = direction.normalized;
        Gizmos.DrawRay(transform.position, direction * m_gizmoScale);

    }

}
