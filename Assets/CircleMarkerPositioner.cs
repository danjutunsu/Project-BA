using UnityEngine;

public class CircleMarkerPositioner : MonoBehaviour
{
    public Transform target;

    private void LateUpdate()
    {
        transform.position = target.position;
    }
}
