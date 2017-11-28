using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager gameManager;
    public GameObject playerPrefab;
    public int blocksUsed;

    private string[] seperateObjs = { "|" };
    private string[] seperateData = { ";" };
    private string dataString;

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

        foreach (GameObject go in BlockManager.blockManager.GetList())
        {
            SaveData data = new SaveData();
            data.objectName = go.name();
            data.position = go.transform.position;
            data.rotation = go.transform.rotation;
        }

        //string[] objects = new string[objectAmount];

        ////Seperate the data string into strings marking each object
        //objects = objectData.Split(seperateObjs, System.StringSplitOptions.RemoveEmptyEntries);

        //string objectDataString = "";
        //objects = new string[BlockManager.blockManager.GetList().Count + 1];

        ////Re-combine the string into a continuous string of data seperated with ';' characters
        //foreach (string str in objects)
        //{
        //    objectDataString += str;
        //}

        ////Split the data once more so it is iterable
        //objects = new string[objects.Length * 8];
        //objects = objectDataString.Split(seperateData, System.StringSplitOptions.RemoveEmptyEntries);

        //for (int i = 0; i < objectAmount * 8; i++)
        //{
        //    SaveData data = new SaveData();
        //    if (i % 8 == 0 || i == 0)
        //    {
        //        data.objectName = objects[i];
        //    }
        //    else if (i == 1 || i % 9 == 0)
        //    {
        //        data.position = new Vector3(
        //            (float)System.Convert.ToDouble(objects[i]), 
        //            (float)System.Convert.ToDouble(objects[i + 1]), 
        //            (float)System.Convert.ToDouble(objects[i + 2])
        //        );
        //        i += 3;
        //    }
        //    else if (i % 4 == 0)
        //    {
        //        data.rotation = new Quaternion(
        //            (float)System.Convert.ToDouble(objects[i]), 
        //            (float)System.Convert.ToDouble(objects[i + 1]), 
        //            (float)System.Convert.ToDouble(objects[i + 2]), 
        //            (float)System.Convert.ToDouble(objects[i + 3])
        //        );
        //        i += 4;
        //    }
        //    objs.Add(data);
        //}
        bf.Serialize(file, objs);
        file.Close();
    }

    public void Load(string filename)
    {
        if (File.Exists(Application.persistentDataPath + "/" + filename + ".dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/" + filename + ".dat", FileMode.Open);
            List<SaveData> data = (List<SaveData>)bf.Deserialize(file);
            SceneManager.loadScene(filename);
            for (int i = 0; i < data.Count - 2; i++)
            {
                GameObject loadedObj = Instantiate(Resources.Load("Prefabs/Placeables/" + data[i].objectName, typeof(GameObject)), data[i].position, data[i].rotation) as GameObject;
                BlockManager.blockManager.AddBlock(loadedObj);
            }
            GameObject player = Instantiate(Resources.Load("Prefabs/Player/" + data[data.Count - 1].objectName, typeof(GameObject)), data[data.Count - 1].position, data[data.Count - 1].rotation) as GameObject;
            file.Close();
        }
    }

    public void SetDataString()
    {
        List<GameObject> objList = BlockManager.blockManager.GetList();
        foreach (GameObject go in objList)
        {
            dataString += go.name + ";";

            dataString += go.transform.position.x + ";";
            dataString += go.transform.position.y + ";";
            dataString += go.transform.position.z + ";";

            dataString += go.transform.rotation.x + ";";
            dataString += go.transform.rotation.y + ";";
            dataString += go.transform.rotation.z + ";";
            dataString += go.transform.rotation.w + ";";
        }

        if (playerPrefab != null)
        {
            dataString += playerPrefab.name + ";";

            dataString += playerPrefab.transform.position.x + ";";
            dataString += playerPrefab.transform.position.y + ";";
            dataString += playerPrefab.transform.position.z + ";";

            dataString += playerPrefab.transform.rotation.x + ";";
            dataString += playerPrefab.transform.rotation.y + ";";
            dataString += playerPrefab.transform.rotation.z + ";";
            dataString += playerPrefab.transform.rotation.w + ";";
        }
    }

    public string GetDataString()
    {
        return dataString;
    }
}

[System.Serializable]
class SaveData
{
    public Vector3 position;
    public Quaternion rotation;
    public string objectName;
}
