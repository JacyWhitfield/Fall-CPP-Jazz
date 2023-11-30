using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // The target to follow (the player's transform)
    public float smoothSpeed = 0.125f; // The smoothness of camera movement
    public Vector3 offset; // The offset from the target's position

    void LateUpdate()
    {
        if (target == null)
        {
            return; // Ensure the target is not null
        }

        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}
