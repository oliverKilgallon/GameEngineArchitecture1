using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour {
    
    public GameObject playerPrefab;
    public delegate void PlayerSpawn();
    public static event PlayerSpawn playerCreate;
    void Start ()
    {
        Instantiate(playerPrefab, transform.position, Quaternion.identity);
        if (playerCreate != null)
        {
            playerCreate();
        }
	}
}
