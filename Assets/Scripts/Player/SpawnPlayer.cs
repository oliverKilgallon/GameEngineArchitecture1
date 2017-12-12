using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour {

    public delegate void playerCreateEvent();
    public static event playerCreateEvent playerCreate;

    public GameObject playerPrefab;

    void awake()
    {
        UIScripts.newGameEvent += CreatePlayer;
    }

    void OnDisable()
    {
        UIScripts.newGameEvent -= CreatePlayer;
    }

    public void CreatePlayer()
    {
        GameObject player = Instantiate(playerPrefab, transform.position, Quaternion.identity);
        player.name = playerPrefab.name;
        Destroy(gameObject);
    }
}
