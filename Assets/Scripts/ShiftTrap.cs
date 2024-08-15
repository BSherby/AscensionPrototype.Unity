using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShiftTrap : Trap
{
    public float teleportDistance = 25f;
    public LayerMask playerLayer;

    private Vector3 platformPosition;
    private float platformHeight;
    
    // Start is called before the first frame update
    void Start()
    {
        platformPosition = transform.position;
        platformHeight = platformPosition.y;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (IsPlayerLayer(collision.gameObject.layer))
        {
            TeleportPlayer(collision.gameObject);
        }
    }

    private bool IsPlayerLayer(int layer)
    {
        return playerLayer == (playerLayer | (1 << layer));
    }

    private void TeleportPlayer(GameObject player)
    {
        Vector3 randomDirection = Random.insideUnitSphere * teleportDistance;
        randomDirection.y = Mathf.Abs(randomDirection.y);

        Vector3 newPosition = platformPosition + randomDirection;

        if (newPosition.y < platformHeight)
        {
            newPosition.y = platformHeight;
        }

        newPosition.y = Mathf.Min(newPosition.y, platformHeight + 25f);

        player.transform.position = newPosition;

        Debug.Log($"Player teleported to {newPosition}");
    }
}
