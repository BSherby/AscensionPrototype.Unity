using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public GameObject player;
    public Transform spawnPoint;
    public TrapManagerExperimental trapManager;

    private TestMovement playerMovement;

    private void Start()
    {
        playerMovement = player.GetComponent<TestMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            RespawnPlayer();
            trapManager.ResetTraps();
        }
    }

    void RespawnPlayer()
    {
        player.transform.position = spawnPoint.position;
        player.transform.rotation = spawnPoint.rotation;

        Rigidbody rb = player.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        if (playerMovement != null)
        {
            playerMovement.ResetMovementStats();
        }
        Debug.Log("Player respawned and movement stats have been reset.");
    }
}
