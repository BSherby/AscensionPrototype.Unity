using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapManagerExperimental : MonoBehaviour
{
    public GameObject fourTileTriggerPrefab;
    public GameObject fourTileSurroundPrefab;
    public GameObject[] trapPrefabs;
    public GameObject[] trapPlatforms;

    public Transform[] platformSpawnPoints;

    private List<GameObject> currentTraps = new List<GameObject>();

    private void Start()
    {
        Debug.Log("TrapManagerExperimental started. Initializing and assigning traps...");

        InstantiateAndAssignTraps();
    }

    public void ResetTraps()
    {
        Debug.Log("Resetting traps...");

        foreach (var platform in currentTraps)
        {
            Debug.Log($"Destroying platform or trap at position {platform.transform.position}");
            Destroy(platform);
        }
        currentTraps.Clear();

        InstantiateAndAssignTraps();
    }

    private void InstantiateAndAssignTraps()
    {
        Debug.Log("Instantiating and assigning traps to platforms...");

        for (int i = 0; i < trapPlatforms.Length; i++)
        {
            int randomIndex = Random.Range(0, trapPrefabs.Length);
            GameObject trapInstance = Instantiate(trapPrefabs[randomIndex], trapPlatforms[i].transform);
            trapInstance.transform.localPosition = Vector3.zero;
            trapInstance.transform.localRotation = Quaternion.identity;
            currentTraps.Add(trapInstance);

            Trap trapComponent = trapInstance.GetComponent<Trap>();
            if (trapComponent != null)
            {
                trapComponent.ActivateTrap();
            }

            Debug.Log($"Trap {trapPrefabs[randomIndex].name} added to platform {trapPlatforms[i].name}");
        }

        foreach (var spawnPoint in platformSpawnPoints)
        {
            if (spawnPoint.CompareTag("FourTileTrigger"))
            {
                GameObject platform = Instantiate(fourTileTriggerPrefab, spawnPoint.position, spawnPoint.rotation);
                currentTraps.Add(platform);
                Debug.Log($"FourTileTrigger platform instantiated at {spawnPoint.position}");
            }
            else if (spawnPoint.CompareTag("FourTileSurround"))
            {
                GameObject platform = Instantiate(fourTileSurroundPrefab, spawnPoint.position, spawnPoint.rotation);
                currentTraps.Add(platform);
                Debug.Log($"FourTileSurround platform instantiated at {spawnPoint.position}");
            }
            else
            {
                Debug.LogWarning($"Platform at position {spawnPoint.position} has an unrecognized tag.");
            }
        }
    }
}
