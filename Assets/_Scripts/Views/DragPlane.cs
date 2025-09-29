using UnityEngine;

[ExecuteAlways]
public class DragPlane : MonoBehaviour
{
    [SerializeField] float size = 5f;
    [SerializeField] Color planeColor = new Color(0f, 1f, 1f, 0.2f);
    public Plane RayPlane;

    void Start()
    {
        RayPlane = new(transform.up, transform.position);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = planeColor;

        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.DrawCube(Vector3.zero, new Vector3(size, 0.01f, size));
        Gizmos.matrix = Matrix4x4.identity;
    }
}
