using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Reference to the player GameObject.
    public GameObject player;

    // The distance between the camera and the player.
    private Vector3 offset;

    // Mouse sensitivity
    public float mouseSensitivity = 200f;

    // Vertical rotation limits
    public float minY = -30f;
    public float maxY = 60f;

    private float rotX;
    private float rotY;

    void Start()
    {
        // Calculate the initial offset
        offset = transform.position - player.transform.position;

        // Initialize rotation based on current camera rotation
        Vector3 angles = transform.eulerAngles;
        rotX = angles.y;
        rotY = angles.x;

        // Lock and hide cursor
        Cursor.lockState = CursorLockMode.Locked;
    }

    void LateUpdate()
    {
        // Mouse movement
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        rotX += mouseX;
        rotY -= mouseY;

        // Clamp vertical rotation
        rotY = Mathf.Clamp(rotY, minY, maxY);

        // Create rotation
        Quaternion rotation = Quaternion.Euler(rotY, rotX, 0);

        // Apply rotation to offset
        Vector3 newOffset = rotation * offset;

        // Set camera position
        transform.position = player.transform.position + newOffset;

        // Always look at player
        transform.LookAt(player.transform.position);
    }
}