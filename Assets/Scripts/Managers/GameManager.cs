using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameManager : MonoBehaviour
{

    public GameObject playerPrefab;
    public Transform spawnPoint;
    public TrapManagerExperimental trapManager; //name 'TrapManagerEperimental' will need to be changed when scripts are changed.
    public ThirdPersonCamera cameraScript;

    private GameObject currentPlayer;

    private void Start()
    {
        RespawnPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            RespawnPlayer();
            trapManager.ResetTraps();
        }
    }

    public void RespawnPlayer()
    {
        if (currentPlayer != null)
        {
            Destroy(currentPlayer);
        }

        currentPlayer = Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);

        if (cameraScript != null)
        {
            cameraScript.AssignPlayer(currentPlayer.transform);
        }
    }
}
