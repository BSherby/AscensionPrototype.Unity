using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapManagerExperimental : MonoBehaviour
{
    public GameObject[] platforms;
    public LayerMask fourTileLayer;
    public LayerMask fourTileSurroundLayer;
    public LayerMask platformLayer;

    public GameObject[] otherTrapPrefabs;

    private Vector3[] initialPositions;
    private Quaternion[] initialRotations;
    private GameObject[] currentTraps;

    private void Start()
    {
        initialPositions = new Vector3[platforms.Length];
        initialRotations = new Quaternion[platforms.Length];
        currentTraps = new GameObject[platforms.Length];

        for (int i=0; i < platforms.Length; i++)
        {
            initialPositions[i] = platforms[i].transform.position;
            initialRotations[i] = platforms[i].transform.rotation;
        }

        AssignTrapsToPlatforms();
    }

    public void AssignTrapsToPlatforms()
    {
        for (int i=0; i < platforms.Length; i++)
        {
            GameObject platform = platforms[i];

            if (IsFourTileLayer(platform.layer))
            {
                if (platform.GetComponent<FourTile>() == null)
                {
                    FourTile trapComponent = platform.AddComponent<FourTile>();
                    trapComponent.playerLayer = LayerMask.GetMask("Player");
                    trapComponent.fourTileSurroundLayer = fourTileSurroundLayer;
                }

                currentTraps[i] = platform;
                Debug.Log($"FourTile trap added to platform {platform.name} on '4Tile' layer.");
            }

            else if (IsFourTileSurroundLayer(platform.layer))
            {
                currentTraps[i] = platform;
                Debug.Log($"Surrounding title added to {platform.name} on 'FourTileSurround' layer.");
            }

            else if (IsPlatformLayer (platform.layer))
            {
                if (platform.GetComponentInChildren<Trap>() == null)
                {
                    int randomIndex = Random.Range(0, otherTrapPrefabs.Length);
                    GameObject trapInstance = Instantiate(otherTrapPrefabs[randomIndex], platform.transform);
                    trapInstance.transform.localPosition = Vector3.zero;
                    trapInstance.transform.localRotation = Quaternion.identity;
                    currentTraps[i] = trapInstance;

                    Debug.Log($"Trap {otherTrapPrefabs[randomIndex].name} added to platform {platform.transform} on 'Platform' layer.");
                }
            }
        }
    }

    public void ResetTraps()
    {
        for (int i=0; i < platforms.Length; i++)
        {
            platforms[i].transform.position = initialPositions[i];
            platforms[i].transform.rotation = initialRotations[i];

            Rigidbody rb = platforms[i].GetComponent<Rigidbody>();
            if (rb != null)
            {
                Debug.Log($"Removing Rigidbody from platform {platforms[i].name}");
                Destroy(rb);
            }
            else
            {
                Debug.Log($"No Rigidbody found on {platforms[i].name}");
            }

            platforms[i].SetActive(true);
        }
        foreach (GameObject trap in currentTraps)
        {
            if (trap != null)
            {
                Trap trapComponent = trap.GetComponent<Trap>();
                if (trapComponent != null)
                {
                    Debug.Log($"Deactivating trap on platform {trap.name}");
                    trapComponent.DeactivateTrap();
                }
                else
                {
                    Debug.Log($"Destroying non-trap object{trap.name}");
                    Destroy(trap);
                }
            }
            else
            {
                Debug.Log("Encountered a null trap in the currentTraps array.");
            }
        }

        for (int i=0; i < currentTraps.Length; i++)
        {
            currentTraps[i] = null;
        }

        Debug.Log("Reassigning traps to platforms.");
        AssignTrapsToPlatforms();
    }

    private bool IsFourTileLayer(int layer)
    {
        return fourTileLayer == (fourTileLayer | (1 << layer));
    }

    private bool IsFourTileSurroundLayer(int layer)
    {
        return fourTileSurroundLayer == (fourTileSurroundLayer | (1 << layer));
    }

    private bool IsPlatformLayer(int layer)
    {
        return platformLayer == (platformLayer | (1 << layer));
    }
}
