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

    //public float turnSmoothTime = 0.1f;

    private Transform cam;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        cam = Camera.main != null ? Camera.main.transform : null;

        ResetMovementStats();
    }

    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = cam.forward * moveVertical + cam.right * moveHorizontal;

        movement.y = 0;

        transform.Translate(movement * speed * Time.deltaTime, Space.World);

        if(Input.GetButtonDown("Jump") && isGrounded)
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
