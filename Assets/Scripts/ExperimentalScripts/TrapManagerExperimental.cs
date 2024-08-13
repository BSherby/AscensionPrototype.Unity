using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapManagerExperimental : MonoBehaviour
{
    public GameObject[] platforms;

    private Vector3[] initialPositions;
    private Quaternion[] initialRotations;
    private GameObject[] currentTraps;

    private void Start()
    {
        initialPositions = new Vector3[platforms.Length];
        initialRotations = new Quaternion[platforms.Length];
        currentTraps = new GameObject[platforms.Length];

        for (int i = 0; i < platforms.Length; i++)
        {
            initialPositions[i] = platforms[i].transform.position;
            initialRotations[i] = platforms[i].transform.rotation;

            Rigidbody rb = platforms[i].GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = true;
            }
        }

        AssignTrapsToPlatforms();
    }

    public void ResetTraps()
    {
        GameObject[] surroundingPlatforms = GameObject.FindGameObjectsWithTag("FourTileSurround");
        foreach(GameObject platform in surroundingPlatforms)
        {
            Destroy(platform);
        }

        StartCoroutine(ReinstantiateSurroundPlatforms());
    }

    private IEnumerator ReinstantiateSurroundPlatforms()
    {
        yield return new WaitForEndOfFrame();

        for (int i = 0; i < platforms.Length; i++)
        {
            if (platforms[i].CompareTag("FourTileSurround"))
            {
                GameObject platform = Instantiate(platforms[i], initialPositions[i], initialRotations[i]);
                platforms[i] = platform;

                Rigidbody rb = platform.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.isKinematic = true;
                }
            }

            if(platforms[i].CompareTag("FourTileTrigger") && !platforms[i].activeSelf)
            {
                platforms[i].SetActive(true);
            }
        }

        AssignTrapsToPlatforms();
    }

    public void AssignTrapsToPlatforms()
    {
        for (int i = 0; i < platforms.Length; i++)
        {
            GameObject platform = platforms[i];

            if (platform.CompareTag("FourTileTrigger"))
            {
                if(platform.GetComponent<FourTile>() == null)
                {
                    FourTile trapComponent = platform.AddComponent<FourTile>();
                    trapComponent.playerLayer = LayerMask.GetMask("Player");
                }

                currentTraps[i] = platform;
                Debug.Log($"FourTile trap added to platform {platform.name}.");
            }
        }
    }

}
