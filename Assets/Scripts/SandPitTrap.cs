using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandPitTrap : Trap
{
    public LayerMask playerLayer;
    public float deathDelay = 5f;

    private TestMovement playerMovement;
    private bool isActivated = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (IsPlayerLayer(collision.gameObject.layer) && !isActivated)
        {
            playerMovement = collision.gameObject.GetComponent<TestMovement>();

            if (playerMovement != null)
            {
                StartCoroutine(ActivateTrap(collision.gameObject));
            }
        }
    }

    private bool IsPlayerLayer(int layer)
    {
        return playerLayer == (playerLayer | (1 << layer));
    }

    private IEnumerator ActivateTrap(GameObject player)
    {
        isActivated = true;
        playerMovement.enabled = false;

        Rigidbody rb = player.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        yield return new WaitForSeconds(deathDelay);

        Destroy(player);

        Debug.Log("Player has been killed by the trap");
    }
}
