using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;

    public float rotationSpeed = 5f;
    public float cameraDistance = 5f;

    private float pitch = 0f;
    private float yaw = 0f;
    
    // Start is called before the first frame update
    void Start()
    {
        if (offset == Vector3.zero)
        {
            offset = new Vector3(0, 1, -cameraDistance);  //Base offset, adjust if more space needed.
        }

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
            return;

        float mouseX = Input.GetAxis("Mouse X") * rotationSpeed;
        float mouseY = Input.GetAxis("Mouse Y") * rotationSpeed;

        yaw += mouseX;
        pitch -= mouseY;
        pitch -= Mathf.Clamp(pitch, -35f, 60f);

        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0f);
        Vector3 cameraPosition = player.position + rotation * offset;

        transform.position = cameraPosition;

        transform.LookAt(player.position);
    }

    public void AssignPlayer(Transform playerTransform)
    {
        player = playerTransform;
    }
}
