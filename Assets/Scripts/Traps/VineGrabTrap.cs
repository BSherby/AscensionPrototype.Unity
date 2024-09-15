using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VineGrabTrap : Trap
{
    public LayerMask playerLayer;
    public float freezeDelay = 1f;
    public float cooldownTime = 5f;

    private TestMovement playerMovement;  //Reference to the movement script
    private bool isFrozen = false;
    private bool isOnCoolDown = false;
    private int requiredPresses;  //Amount of presses needed to free the player
    private int currentPresses = 0;  //Counter for the amount pressed currently

    public override void ActivateTrap()
    {
        Debug.Log("VineGrabTrap Activated");
    }

    public override void DeactivateTrap()
    {
        Debug.Log("VineGrabTrap Deactivated");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (IsPlayerLayer(collision.gameObject.layer) && !isFrozen && !isOnCoolDown)
        {
            playerMovement = collision.gameObject.GetComponent<TestMovement>();

            if (playerMovement != null)
            {
                StartCoroutine(FreezePlayer());
            }
        }
    }

    private bool IsPlayerLayer(int layer)
    {
        return playerLayer == (playerLayer | (1 << layer));
    }

    private IEnumerator FreezePlayer()
    {
        //Freezes the player on contact and disables the movement
        isFrozen = true;
        playerMovement.enabled = false;

        //Sets delay before the spacebar can be pressed to free player
        yield return new WaitForSeconds(freezeDelay);

        requiredPresses = Random.Range(5, 26);

        Debug.Log($"Player must press the spacebar {requiredPresses} times to be freed.");

        while (currentPresses < requiredPresses)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                currentPresses++;
                Debug.Log($"Spacebar pressed {currentPresses}/{requiredPresses} times.");
            }
            yield return null;
        }

        isFrozen = false;
        playerMovement.enabled = true;

        currentPresses = 0;

        Debug.Log("Player is freed and can move again.");

        StartCoroutine(Cooldown());
    }

    private IEnumerator Cooldown()
    {
        isOnCoolDown = true;
        Debug.Log("Trap is now on cooldown");

        yield return new WaitForSeconds(cooldownTime);

        isOnCoolDown = false;
        Debug.Log("Trap is ready to be triggered again");
    }
}
