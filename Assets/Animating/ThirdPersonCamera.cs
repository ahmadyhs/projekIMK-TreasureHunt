using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform target; // The target object to follow
    public float sensitivity = 2f; // Mouse sensitivity
    public float distance = 5f; // Distance from the target
    public float height = 2f; // Height of the camera
    public float zoomSpeed = 5f; // Speed of zooming
    public float verticalSpeed = 2f; // Speed of vertical movement

    private float yaw = 0f; // Horizontal rotation angle
    private float pitch = 0f; // Vertical rotation angle
    private float currentDistance; // Current distance from the target
    public float keyBoardSensitivity = 100f;
    private Vector3 keyboardOffset;
    public float zoomLimit = 50f;

    private void Start()
    {
        currentDistance = distance;
    }

    private void Update()
    {
        // Read mouse input
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        keyboardOffset += new Vector3(0f, Input.GetAxisRaw("eq") * Time.deltaTime * keyBoardSensitivity, 0f) ;
        //Debug.Log(keyboardOffset);

        // Update the camera's rotation based on mouse movement
        yaw += mouseX * sensitivity;
        pitch -= mouseY * verticalSpeed;
        pitch = Mathf.Clamp(pitch, -20f, 60f); // Limit vertical movement

        // Zoom in/out based on scroll wheel input
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        currentDistance -= scroll * zoomSpeed;
        currentDistance = Mathf.Clamp(currentDistance, 1f, zoomLimit); // Limit zoom range

        // Calculate the new camera position based on target's position and rotation
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0f);
        Vector3 offset = rotation * new Vector3(0f, height, -currentDistance);
        Vector3 targetPosition = target.position + offset + keyboardOffset;

        // Update the camera's position and look at the target
        transform.position = targetPosition;
        transform.LookAt(target.position + keyboardOffset);
    }
}
