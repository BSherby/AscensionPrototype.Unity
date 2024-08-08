using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapManager : MonoBehaviour
{
    public GameObject[] trapPrefabs;
    public GameObject[] platforms;

    private Vector3[] initialPositions;
    private Quaternion[] initialRotations;
    private GameObject[] currentTraps;

    private void Start()
    {
        //Initializes arrays based on the number of platforms
        initialPositions = new Vector3[platforms.Length];
        initialRotations = new Quaternion[platforms.Length];
        currentTraps = new GameObject[platforms.Length];

        for (int i=0; i < platforms.Length; i++)
        {
            initialPositions[i] = platforms[i].transform.position;
            initialRotations[i] = platforms[i].transform.rotation;
        }

        AssignRandomTraps();
    }

    public void AssignRandomTraps()
    {
        for (int i = 0; i < currentTraps.Length; i++)
        {
            if (currentTraps[i] != null)
            {
                Destroy(currentTraps[i]);
                currentTraps[i] = null;
            }
        }

        for (int i = 0; i < platforms.Length; i++)
        {
            int randomIndex = Random.Range(0, trapPrefabs.Length);
            GameObject selectedTrap = Instantiate(trapPrefabs[randomIndex], platforms[i].transform);
            selectedTrap.transform.localPosition = Vector3.zero;
            selectedTrap.transform.localRotation = Quaternion.identity;
            currentTraps[i] = selectedTrap;

            Trap trapComponent = selectedTrap.GetComponent<Trap>();
            if (trapComponent != null)
            {
                trapComponent.ActivateTrap();
                Debug.Log($"Platform {platforms[i].name} has {trapPrefabs[randomIndex].name} applied and activated");
            }
            else
            {
                Debug.LogWarning($"Platform {platforms[i].name} has {trapPrefabs[randomIndex].name} applied but no Trap component found.");
            }
        }
    }

    public void ResetTraps()
    {
        for (int i = 0; i < platforms.Length; i++)
        {
            platforms[i].transform.position = initialPositions[i];
            platforms[i].transform.rotation = initialRotations[i];
        }

        AssignRandomTraps();
    }
}
