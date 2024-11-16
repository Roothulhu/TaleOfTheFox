using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform Player;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    void Start()
    {
        if (Player != null)
        {
            offset = transform.position - Player.position;
        }
    }

    void LateUpdate()
    {
        if (Player == null) return;
        Vector3 desiredPosition = Player.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = new Vector3(smoothedPosition.x, smoothedPosition.y, transform.position.z);
    }
}
