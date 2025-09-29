using UnityEngine;

public class MouseRaycastSystem : Singleton<MouseRaycastSystem>
{
    [SerializeField] DragPlane _dragPlane;

    public Vector3 GetMouseOnPlane() //float height = 0f // TODO changee to offset
    {
        //Ray ray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (_dragPlane.RayPlane.Raycast(ray, out float distance))
        {
            return ray.GetPoint(distance);
        }
        return Vector3.zero;
    }
}
