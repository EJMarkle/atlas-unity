using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public float distance = 5.0f;
    public float height = 2.0f;
    public float rotationSpeed = 2.0f;

    private float currentRotationAngle;
    private float currentHeight;

    void LateUpdate()
    {
        // Calculate rotation angles and height
        currentRotationAngle += Input.GetAxis("Mouse X") * rotationSpeed;
        currentHeight -= Input.GetAxis("Mouse Y") * rotationSpeed;

        // limit camera height
        currentHeight = Mathf.Clamp(currentHeight, -5f, 5f);

        // Convert angle into a rotation
        Quaternion currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);

        // Set camera transform to player transform
        transform.position = player.position;
        transform.position -= currentRotation * Vector3.forward * distance;

        // set camera height
        transform.position = new Vector3(transform.position.x, player.position.y + height + currentHeight, transform.position.z);

        // Always look at the target
        transform.LookAt(player);
    }
}
