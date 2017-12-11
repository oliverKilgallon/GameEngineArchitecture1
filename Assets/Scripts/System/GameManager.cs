using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager gameManager;

    public delegate void LevelLoaded(int playerBlocks);
    public static event LevelLoaded levelLoaded;
    
    public GameObject playerPrefab;
    public int blocksUsed;

	void Awake ()
    {
        if (gameManager == null)
        {
            DontDestroyOnLoad(gameObject);
            gameManager = this;
        } else if (gameManager != this)
        {
            Destroy(gameObject);
        }
	}

    public void Save(string filename)
    {

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/" + filename + ".dat");

        List<SaveData> objs = new List<SaveData>();
        
        List<GameObject> objects = BlockManager.blockManager.GetList();

        if (objects == null)
        {
            Debug.Log("No data to save!");
        }
        else
        {
            foreach (GameObject go in objects)
            {
                SaveData data = new SaveData();
                data.objectName = go.name;
                data.positionX = go.transform.position.x;
                data.positionY = go.transform.position.y;
                data.positionZ = go.transform.position.z;
                                 
                data.rotationX = go.transform.rotation.x;
                data.rotationY = go.transform.rotation.y;
                data.rotationZ = go.transform.rotation.z;
                data.rotationW = go.transform.rotation.w;
                objs.Add(data);
            }

            SaveData playerData = new SaveData();
            playerData.objectName = playerPrefab.name;
            playerData.positionX = playerPrefab.transform.position.x;
            playerData.positionY = playerPrefab.transform.position.y;
            playerData.positionZ = playerPrefab.transform.position.z;

            playerData.rotationX = playerPrefab.transform.rotation.x;
            playerData.rotationY = playerPrefab.transform.rotation.y;
            playerData.rotationZ = playerPrefab.transform.rotation.z;
            playerData.rotationW = playerPrefab.transform.rotation.w;
            objs.Add(playerData);
        }

        bf.Serialize(file, objs);
        file.Close();
    }

    public void Load(string filename)
    {
        Debug.Log("Loading file at: " + Application.persistentDataPath + "/" + filename + ".dat");
        if (File.Exists(Application.persistentDataPath + "/" + filename + ".dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();

            FileStream file = File.Open(Application.persistentDataPath + "/" + filename + ".dat", FileMode.Open);



            //foreach (SaveData sd in data)
            //{
            //    GameObject obj = Instantiate(
            //        Resources.Load("Prefabs/Placeables/" + sd.objectName, typeof(GameObject)),
            //        new Vector3(sd.positionX, sd.positionY, sd.positionZ),
            //        new Quaternion(sd.rotationX, sd.rotationY, sd.rotationZ, sd.rotationW)
            //    ) as GameObject;
            //    BlockManager.blockManager.GetList().Add(obj);
            //}

            if (SceneManager.GetActiveScene().name != "MainMenu")
            {
                List<GameObject> blockList = BlockManager.blockManager.GetList();
                if (blockList != null)
                {
                    //Destroy any duplicates already present
                    foreach (GameObject obj in BlockManager.blockManager.GetList())
                    {
                        Destroy(obj);
                    }
                }

                //Clear blocklist to prevent duplication in the list
                BlockManager.blockManager.GetList().Clear();
            }
            else
            {
                SceneManager.LoadScene(filename);
            }

            

            List<SaveData> data = (List<SaveData>)bf.Deserialize(file);

            //Instantiate all blocks in the blocklist, aside from the player
            for (int i = 0; i < data.Count - 2; i++)
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

                if (BlockManager.blockManager != null) BlockManager.blockManager.AddBlock(loadedObj);
            }
            
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
}

[System.Serializable]
class SaveData
{
    public float positionX;
    public float positionY;
    public float positionZ;

    public float rotationX;
    public float rotationY;
    public float rotationZ;
    public float rotationW;

    public string objectName;
}
