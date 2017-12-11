using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager gameManager;
<<<<<<< HEAD

    public delegate void LevelLoaded(int playerBlocks);
    public static event LevelLoaded levelLoaded;

    public int blocksUsed;
<<<<<<< HEAD
=======
    public GameObject playerPrefab;
    public int blocksUsed;

    private string[] seperateObjs = { "|" };
    private string[] seperateData = { ";" };
    private string dataString;
>>>>>>> a2bdaefb215ed6ce4a9bd7e9ce5f9cb73c8690d6
=======
    private bool overwriteSave = false;
>>>>>>> 0954e3ba608dd68fcd98f852c30916b91a530a22

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
        

        List<SaveData> objs = new List<SaveData>();

<<<<<<< HEAD
        List<GameObject> objects = BlockManager.blockManager.GetList();

        if (objects == null)
        {
            Debug.Log("No data to save!");
        }
        else
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(Application.persistentDataPath + "/" + filename + ".dat");

            foreach (GameObject obj in objects)
            {
                SaveData data = new SaveData();
                data.objectName = obj.name;
                data.positionX = obj.transform.position.x;
                data.positionY = obj.transform.position.y;
                data.positionZ = obj.transform.position.z;

                data.rotationX = obj.transform.rotation.x;
                data.rotationY = obj.transform.rotation.y;
                data.rotationZ = obj.transform.rotation.z;
                data.rotationW = obj.transform.rotation.w;
                objs.Add(data);
            }
            bf.Serialize(file, objs);
            file.Close();
        }

        
=======
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
>>>>>>> a2bdaefb215ed6ce4a9bd7e9ce5f9cb73c8690d6
    }

    public void Load(string filename)
    {
        Debug.Log("Loading file at: " + Application.persistentDataPath + "/" + filename + ".dat");
        if (File.Exists(Application.persistentDataPath + "/" + filename + ".dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();

            FileStream file = File.Open(Application.persistentDataPath + "/" + filename + ".dat", FileMode.Open);

            List<SaveData> data = (List<SaveData>)bf.Deserialize(file);
<<<<<<< HEAD

            foreach (GameObject obj in BlockManager.blockManager.GetList())
            {
                Destroy(obj);
            }

            BlockManager.blockManager.GetList().Clear();

            foreach (SaveData sd in data)
            {
                GameObject obj = Instantiate(
                    Resources.Load("Prefabs/Placeables/" + sd.objectName , typeof(GameObject)), 
                    new Vector3(sd.positionX, sd.positionY, sd.positionZ), 
                    new Quaternion(sd.rotationX, sd.rotationY, sd.rotationZ, sd.rotationW)
                ) as GameObject;
                BlockManager.blockManager.GetList().Add(obj);
=======
            SceneManager.loadScene(filename);
            for (int i = 0; i < data.Count - 2; i++)
            {
                GameObject loadedObj = Instantiate(Resources.Load("Prefabs/Placeables/" + data[i].objectName, typeof(GameObject)), data[i].position, data[i].rotation) as GameObject;
                BlockManager.blockManager.AddBlock(loadedObj);
>>>>>>> a2bdaefb215ed6ce4a9bd7e9ce5f9cb73c8690d6
            }
            GameObject player = Instantiate(Resources.Load("Prefabs/Player/" + data[data.Count - 1].objectName, typeof(GameObject)), data[data.Count - 1].position, data[data.Count - 1].rotation) as GameObject;
            file.Close();
        }
        
        if (levelLoaded != null)
        {
<<<<<<< HEAD
            blocksUsed = BlockManager.blockManager.GetList().Count;
            levelLoaded(blocksUsed);
=======
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
>>>>>>> a2bdaefb215ed6ce4a9bd7e9ce5f9cb73c8690d6
        }
    }

    public void SetOverwriteSave(bool overwriteSave)
    {
        this.overwriteSave = overwriteSave;
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
