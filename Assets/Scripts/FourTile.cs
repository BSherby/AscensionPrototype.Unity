using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FourTile : Trap
{
    public float fallDelay = 0.02f;
    public float destroyDelay = 5f;
    public LayerMask playerLayer;
    public LayerMask fourTileSurroundLayer;

    private bool isFalling = false;

    private void Start()
    {
        DetectSurroundingPlatforms();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!isFalling && IsPlayerLayer(collision.gameObject.layer))
        {
            StartCoroutine(FallAndDestroyTiles());
        }
    }

    private IEnumerator FallAndDestroyTiles()
    {
        isFalling = true;

        yield return FallAndDestroy(gameObject);

        Collider[] surroundingPlatforms = Physics.OverlapSphere(transform.position, 2f, fourTileSurroundLayer);
        foreach(Collider platform in surroundingPlatforms)
        {
            if (platform.gameObject != this.gameObject)
            {
                yield return FallAndDestroy(platform.gameObject);
            }
        }
    }

    private IEnumerator FallAndDestroy(GameObject title)
    {
        yield return new WaitForSeconds(fallDelay);

        Rigidbody rb = title.AddComponent<Rigidbody>();
        rb.isKinematic = false;

        yield return new WaitForSeconds(destroyDelay);
        title.SetActive(false);
    }

    private bool IsPlayerLayer(int layer)
    {
        return playerLayer == (playerLayer | (1 << layer));
    }

    private void DetectSurroundingPlatforms()
    {
        Collider[] surroundingPlatforms = Physics.OverlapSphere(transform.position, 2f, fourTileSurroundLayer);
        Debug.Log($"Detected {surroundingPlatforms.Length} surrounding platforms.");
    }

    public override void ActivateTrap()
    {
        Debug.Log("FourTile trap activated");
        gameObject.SetActive(true);
    }

    public override void DeactivateTrap()
    {
        Debug.Log("FourTile trap deactivated");
        gameObject.SetActive(false);
    }
}
