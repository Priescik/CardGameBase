using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class BoardFieldsGizmo : MonoBehaviour
{
    [Header("Field Settings")]
    [Tooltip("How many fields to create along X axis")]
    [SerializeField] int _columns = 4;
    [Tooltip("How many fields to create along Z axis")]
    [SerializeField] int _rows = 3;
    [Tooltip("Spacing between fields in X axis")]
    [SerializeField] float _spacingX = 2f;
    [Tooltip("Spacing between fields in Z axis")]
    [SerializeField] float _spacingZ = 2f;

    [SerializeField] bool _splitVertically;

    public float gizmoRadius = 0.3f;

    /// <summary>
    /// Returns an array of world positions for all fields.
    /// </summary>
    public (List<Vector3> worldPositions, List<Vector3> localPositions) GetFieldPositions()
    {
        var positionsA = new List<Vector3>();
        var positionsB = new List<Vector3>();

        for (int r = 0; r < _rows; r++)
        {
            for (int c = 0; c < _columns; c++)
            {
                Vector3 localPos = new Vector3((c-(_columns-1)/2f) * _spacingX, 0, (r-(_rows-1)/2f) * _spacingZ);
                if (_splitVertically)
                {
                    if (localPos.x < 0)
                    {
                        positionsA.Add(transform.TransformPoint(localPos));
                    }
                    else
                    {
                        positionsB.Add(transform.TransformPoint(localPos));
                    }
                }
                else
                {
                    if (localPos.z < 0)
                    {
                        positionsA.Add(transform.TransformPoint(localPos));
                    }
                    else
                    {
                        positionsB.Add(transform.TransformPoint(localPos));
                    }
                }
            }
        }

        return (positionsA, positionsB);
    }

    private void OnDrawGizmos()
    {
        (List<Vector3> sideA, List<Vector3> sideB) = GetFieldPositions();
        Gizmos.color = Color.blue;
        foreach (var pos in sideA)
        {
            Gizmos.DrawSphere(pos, gizmoRadius);
        }
        Gizmos.color = Color.red;
        foreach (var pos in sideB)
        {
            Gizmos.DrawSphere(pos, gizmoRadius);
        }
    }
}
