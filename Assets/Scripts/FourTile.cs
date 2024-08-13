using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FourTile : Trap
{
    public float fallDelay = 0.02f;
    public LayerMask playerLayer;

    private bool isTriggered = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (!isTriggered && IsPlayerLayer(collision.gameObject.layer))
        {
            isTriggered = true;
            StartCoroutine(TriggerFall());
        }
    }

    private IEnumerator TriggerFall()
    {
        yield return new WaitForSeconds(fallDelay);

        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = false;
        }

        GameObject[] surroundingPlatforms = GameObject.FindGameObjectsWithTag("FourTileSurround");
        foreach (GameObject platform in surroundingPlatforms)
        {
            Rigidbody platformRb = platform.GetComponent<Rigidbody>();
            if (platformRb != null)
            {
                platformRb.isKinematic = false;
            }
        }

        yield return new WaitForSeconds(0.1f);
        gameObject.SetActive(false);
    }

    private bool IsPlayerLayer(int layer)
    {
        return playerLayer == (playerLayer | (1 << layer));
    }

    public override void ActivateTrap()
    {
        gameObject.SetActive(true);
    }

    public override void DeactivateTrap()
    {
        gameObject.SetActive(false);
    }
}
