using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BlockManager : MonoBehaviour {

    public static BlockManager blockManager;
    private List<GameObject> playerCreatedBlocks =  new List<GameObject>();
    
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
        };
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= Load;
    }

    void Start()
    {
        //playerCreatedBlocks = new List<GameObject>();
    }

    public static void Load(Scene scene, LoadSceneMode mode)
    {
        //Debug.Log("Loading file at: " + Application.persistentDataPath + "/" + filename + ".dat");
        if (File.Exists(Application.persistentDataPath + "/" + scene.name + ".dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();

            FileStream file = File.Open(Application.persistentDataPath + "/" + scene.name + ".dat", FileMode.Open);
            
            List<SaveData> data = (List<SaveData>)bf.Deserialize(file);

            Debug.Log(SceneManager.GetActiveScene().name);
            //Instantiate all blocks in the blocklist, aside from the player
            for (int i = 0; i < data.Count - 1; i++)
            {
                GameObject loadedObj = Instantiate(
                    Resources.Load("Prefabs/Placeables/" + data[i].objectName, typeof(GameObject)),
                    new Vector3(
                        data[i].positionX,
                        data[i].positionY,
                        data[i].positionZ),
                    new Quaternion(
                        data[i].rotationX,
                        data[i].rotationY,
                        data[i].rotationZ,
                        data[i].rotationW
                        )
                    ) as GameObject;
                
                if (blockManager != null) blockManager.AddBlock(loadedObj);
            }

            //Destroy any duplicate player
            Destroy(GameObject.Find("Player"));

            //Instantiate the player
            GameObject player = Instantiate(
                Resources.Load("Prefabs/Player/" + data[data.Count - 1].objectName, typeof(GameObject)),
                new Vector3(
                    data[data.Count - 1].positionX,
                    data[data.Count - 1].positionY,
                    data[data.Count - 1].positionZ),
                new Quaternion(
                    data[data.Count - 1].rotationX,
                    data[data.Count - 1].rotationY,
                    data[data.Count - 1].rotationZ,
                    data[data.Count - 1].rotationW)
                ) as GameObject;
            file.Close();
        }
    }

    public void AddBlock(GameObject block)
    {
        playerCreatedBlocks.Add(block);
    }

    public void RemoveBlock(int blockID)
    {
        for (int i = 0; i < playerCreatedBlocks.Count; i++)
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
