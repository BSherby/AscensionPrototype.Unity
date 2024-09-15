using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulBindTrap : Trap
{
    public LayerMask playerLayer;
    private TestMovement playerMovement; //Reference to the movement script
    private bool effectActive = false;

    public override void ActivateTrap()
    {
        throw new System.NotImplementedException();
    }

    public override void DeactivateTrap()
    {
        base.DeactivateTrap();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (IsPlayerLayer(collision.gameObject.layer) && !effectActive)
        {
            playerMovement = collision.gameObject.GetComponent<TestMovement>();

            if (playerMovement != null)
            {
                StartCoroutine(ApplyRandomEffect());
            }
        }
    }

    private bool IsPlayerLayer(int layer)
    {
        return playerLayer == (playerLayer | (1 << layer));
    }

    private IEnumerator ApplyRandomEffect()
    {
        //Flag to indicate that an effect is active
        effectActive = true;

        //Assigns a random duration for the effect
        float effectDuration = Random.Range(3.5f, 18f);

        //Randomly sets a speed effect.
        float speedEffect = Random.Range(-3f, 5f);
        playerMovement.speed += speedEffect;
        Debug.Log($"Movement speed adjusted by {speedEffect}. New speed: {playerMovement.speed}");

        //Randomly sets a jump variable.
        float jumpEffect = Random.Range(-4f, 10f);
        playerMovement.jumpStrength += jumpEffect;
        Debug.Log($"Jump strength adjusted by {jumpEffect}. New jump strength: {playerMovement.jumpStrength}");


        yield return new WaitForSeconds(effectDuration);

        //Reverts the effect.
        playerMovement.speed -= speedEffect;
        Debug.Log($"Movement speed restored. Current speed: {playerMovement.speed}");

        playerMovement.jumpStrength -= jumpEffect;
        Debug.Log($"Jump stregth restored. Current jump strength: {playerMovement.jumpStrength}");

        //Flag to indicate the effect has ended
        effectActive = false;
    }
}
