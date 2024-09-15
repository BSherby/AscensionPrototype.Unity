using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTrap : Trap
{
    public LayerMask playerLayer;
    public float minDeathDelay = 0.5f;
    public float maxDeathDelay = 5f;
    private bool isActivated = false;

    public override void ActivateTrap()
    {
        Debug.Log("DeathTrap Activated");
    }

    public override void DeactivateTrap()
    {
        Debug.Log("DeathTrap deactivated");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (IsPlayerLayer(collision.gameObject.layer) && !isActivated)
        {
            StartCoroutine(ActivateDeathTrap(collision.gameObject));
        }
    }

    private IEnumerator ActivateDeathTrap(GameObject player)
    {
        isActivated = true;
        ActivateTrap();

        float randomDelay = Random.Range(minDeathDelay, maxDeathDelay);
        Debug.Log($"DeathTrap will destroy the player after {randomDelay} seconds.");

        yield return new WaitForSeconds(randomDelay);

        Destroy(player);

        Debug.Log("Player has been destroyed by the DeathTrap");
    }

    private bool IsPlayerLayer(int layer)
    {
        return playerLayer == (playerLayer | (1 << layer));
    }
}
