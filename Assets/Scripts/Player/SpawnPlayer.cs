using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour {
    
    public GameObject playerPrefab;
    public delegate void PlayerSpawn();
    public static event PlayerSpawn playerCreate;
    void Start ()
    {
        GameObject player = Instantiate(playerPrefab, transform.position, Quaternion.identity);
        player.name = playerPrefab.name;
        if (playerCreate != null)
        {
            playerCreate();
        }
	}
}
