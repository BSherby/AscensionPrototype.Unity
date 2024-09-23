using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapManagerExpOdds : MonoBehaviour
{
    public GameObject fourTileTriggerPrefab;
    public GameObject fourTileSurroundPrefab;
    public GameObject[] trapPrefabs;
    public int[] trapWeights = new int[] {6,4}; //Corresonds with the order set up in the inspector.
    public GameObject[] trapPlatforms;
    public Transform[] platformSpawnPoints;

    private List<GameObject> currentTraps = new List<GameObject>();
    
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("TrapManagerExperimental started. Initializing and assigning traps");
        
        InstantiateAndAssignTraps();
    }

    public void ResetTraps()
    {

        foreach (var platform in currentTraps)
        {
            Debug.Log($"Destroying pltaform/trap at position {platform.transform.position}");
            Destroy(platform);
        }
        currentTraps.Clear();

        InstantiateAndAssignTraps();
    }

    private void InstantiateAndAssignTraps()
    {
        Debug.Log("Instantiating and assigning traps to platforms");

        for (int i = 0; i < trapPlatforms.Length; i++)
        {
            int selectedIndex = GetWeightedRandomIndex();
            GameObject trapInstance = Instantiate(trapPrefabs[selectedIndex], trapPlatforms[i].transform);
            trapInstance.transform.localPosition = Vector3.zero;
            trapInstance.transform.localRotation = Quaternion.identity;
            currentTraps.Add(trapInstance);

            Debug.Log($"Trap {trapPrefabs[selectedIndex].name} added to platform {trapPlatforms[i].name}");
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

    private int GetWeightedRandomIndex()
    {
        int totalWeight = 0;
        for (int i = 0; i < trapWeights.Length; i++)
        {
            totalWeight += trapWeights[i];
        }

        int randomValue = Random.Range(0, totalWeight);
        for (int i = 0; i < trapWeights.Length; i++)
        {
            if(randomValue < trapWeights[i])
            {
                return i;
            }
            randomValue -= trapWeights[i];
        }

        return 0;
    }
}
