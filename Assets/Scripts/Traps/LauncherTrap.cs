using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LauncherTrap : Trap
{
    public float launchForce = 10f;
    public LayerMask playerLayer;

    private void OnCollisionEnter(Collision collision)
    {
        if (IsPlayerLayer (collision.gameObject.layer))
        {
            Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Vector3 randomDirection = new Vector3(
                    Random.Range(-1f, 1f),
                    Random.Range(0.5f, 1f),
                    Random.Range(-1f, 1f)
                    ).normalized;

                rb.AddForce(randomDirection * launchForce, ForceMode.Impulse);
            }
        }
    }

    public override void ActivateTrap()
    {
        gameObject.SetActive(true);
    }

    public override void DeactivateTrap()
    {
        gameObject.SetActive(false);
    }

    private bool IsPlayerLayer(int layer)
    {
        return playerLayer == (playerLayer | (1 << layer));
    }
}
