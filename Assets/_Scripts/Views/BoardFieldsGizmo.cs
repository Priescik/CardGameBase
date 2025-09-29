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

    public Color gizmoColor = Color.cyan;
    public float gizmoRadius = 0.3f;

    /// <summary>
    /// Returns an array of world positions for all fields.
    /// </summary>
    public Vector3[] GetFieldPositions()
    {
        Vector3[] positions = new Vector3[_rows * _columns];
        int index = 0;

        for (int r = 0; r < _rows; r++)
        {
            for (int c = 0; c < _columns; c++)
            {
                Vector3 localPos = new Vector3((c-(_columns-1)/2f) * _spacingX, 0, (r-(_rows-1)/2f) * _spacingZ);
                positions[index++] = transform.TransformPoint(localPos);
            }
        }

        return positions;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;
        foreach (var pos in GetFieldPositions())
        {
            Gizmos.DrawSphere(pos, gizmoRadius);
        }
    }
}
