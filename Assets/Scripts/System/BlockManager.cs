using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour {

    public static BlockManager blockManager;
    private List<GameObject> playerCreatedBlocks;
    
    void Awake()
    {
        if (blockManager == null)
        {
            DontDestroyOnLoad(gameObject);
            blockManager = this;
        }
        else if (blockManager != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        playerCreatedBlocks = new List<GameObject>();
    }

    public void AddBlock(GameObject block)
    {
        playerCreatedBlocks.Add(block);
    }

    public void RemoveBlock(int blockID)
    {
        for (int i = 0; i < playerCreatedBlocks.Count - 1; i++)
        {
            if (playerCreatedBlocks[i].GetInstanceID() == blockID) playerCreatedBlocks.Remove(playerCreatedBlocks[i]);
        }
    }

    public string GetListItems()
    {
        string objList = "";
        foreach(GameObject go in playerCreatedBlocks)
        {
            objList += go.GetInstanceID().ToString() + ", ";
        }
        return objList;
    }

    public List<GameObject> GetList()
    {
        return playerCreatedBlocks;
    }
}
