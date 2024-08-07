using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapManager : MonoBehaviour
{

    public GameObject[] traps;
    public float trapActivationChance = 0.05f; //Current probability is 1 in 20.

    private Vector3[] initialPositions;
    private Quaternion[] initialRotations;

    private void Start()
    {
        initialPositions = new Vector3[traps.Length];
        initialRotations = new Quaternion[traps.Length];

        for (int i = 0; i < traps.Length; i++)
        {
            initialPositions[i] = traps[i].transform.position;
            initialRotations[i] = traps[i].transform.rotation;
        }
    }

    public void ActivateRandomTrap()
    {
        foreach (GameObject trap in traps)
        {
            Trap trapComponent = trap.GetComponent<Trap>();
            if (trapComponent != null)
            {
                if (Random.value <= trapActivationChance)
                {
                    trapComponent.ActivateTrap();
                }
                else
                {
                    trapComponent.DeactivateTrap();
                }    
            }
        }
    }

    public void ResetTraps()
    {
        for (int i = 0; i < traps.Length; i++)
        {
            traps[i].transform.position = initialPositions[i];
            traps[i].transform.rotation = initialRotations[i];
        }
    }

    public void SetTrapActivationChance(float chance)
    {
        trapActivationChance = Mathf.Clamp01(chance); //Ensures the chance is between 0 & 1.
    }
}
