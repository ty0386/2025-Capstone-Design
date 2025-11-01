using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    private Vector3 offset;
    public float smoothSpeed = 0.125f;

    void Start()
    {
        offset = new Vector3(0, 0, transform.position.z - target.position.z);
    }

    void FixedUpdate() //LateUpdate()로 버벅거림이 있어 FixedUpdate()로 변경
    {
        if (target == null) return;

        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}
