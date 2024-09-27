using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMovement : MonoBehaviour
{
    public float defaultSpeed = 5f;
    public float defaultJumpStrenght = 5f;

    [HideInInspector] public float speed;
    [HideInInspector] public float jumpStrength;

    private bool isGrounded;
    private Rigidbody rb;
    private Transform cam;

    public float rotationSpeed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        cam = Camera.main != null ? Camera.main.transform : null;

        ResetMovementStats();
    }

    void Update()
    {
        HandleMovement();
        HandleJump();
    }

    private void HandleMovement()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        if (cam != null)
        {
            Vector3 movement = cam.forward * moveVertical + cam.right * moveHorizontal;

            movement.y = 0;

            if (movement.magnitude > 1)
            {
                movement.Normalize();
            }

            transform.Translate(movement * speed * Time.deltaTime, Space.World);

            if (movement != Vector3.zero)
            {
                Quaternion toRotation = Quaternion.LookRotation(movement, Vector3.up);
                transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
            }
        }
    }

    private void HandleJump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpStrength, ForceMode.Impulse);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("TrapPlatform"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("TrapPlatform"))
        {
            isGrounded = false;
        }
    }

    public void ResetMovementStats()
    {
        speed = defaultSpeed;
        jumpStrength = defaultJumpStrenght;
        Debug.Log("Player movement speed and jump strength reset to default values.");
    }
}
