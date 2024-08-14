using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapManagerExperimental : MonoBehaviour
{
    public GameObject fourTileTriggerPrefab;
    public GameObject fourTileSurroundPrefab;
    public GameObject[] trapPrefabs;

    public Transform[] platformSpawnPoints;

    private List<GameObject> currentTraps = new List<GameObject>();

    private void Start()
    {
        Debug.Log("TrapManagerExperimental started. Initializing and assigning the traps.");
        
        InstantiateAndAssignTraps();
    }

    public void ResetTraps()
    {
        Debug.Log("Resetting the traps...");

        foreach(var platform in currentTraps)
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
        
        foreach (var spawnPoint in platformSpawnPoints)
        {
            if (spawnPoint.CompareTag("FourTileTrigger"))
            {
                GameObject platform = Instantiate(fourTileTriggerPrefab, spawnPoint.position, spawnPoint.rotation);
                currentTraps.Add(platform);
                Debug.Log($"FourTile platform instantiated at {spawnPoint.position}");
            }
            else if (spawnPoint.CompareTag("FourTileSurround"))
            {
                GameObject platform = Instantiate(fourTileSurroundPrefab, spawnPoint.position, spawnPoint.rotation);
                currentTraps.Add(platform);
                Debug.Log($"FourTileSurround platform instantiated at {spawnPoint.position}");
            }
            else if (spawnPoint.CompareTag("TrapPlatform"))
            {
                int randomIndex = Random.Range(0, trapPrefabs.Length);
                GameObject trapInstance = Instantiate(trapPrefabs[randomIndex], spawnPoint.position, spawnPoint.rotation);
                trapInstance.transform.SetParent(spawnPoint);
                currentTraps.Add(trapInstance);

                Debug.Log($"Trap {trapPrefabs[randomIndex].name} added to platform at position {spawnPoint.position}");
            }
            else
            {
                Debug.LogWarning($"Platform at position {spawnPoint.position} has an unrecognized tag.");
            }
        }
    }
}
