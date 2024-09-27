using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public float mouseSensitivity = 100f;
    public Transform cameraTarget;
    public float distanceFromPlayer = 5f;

    private float pitch = 0f;
    private float yaw = 0f;
    
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        yaw += mouseX;
        pitch -= mouseY;
        pitch = Mathf.Clamp(pitch, -35f, 60f);

        cameraTarget.position = player.position - transform.forward * distanceFromPlayer;
        cameraTarget.rotation = Quaternion.Euler(pitch, yaw, 0f);

        transform.position = cameraTarget.position;
        transform.LookAt(player);
    }
}
