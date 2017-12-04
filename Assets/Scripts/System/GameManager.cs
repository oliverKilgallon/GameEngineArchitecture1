using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager gameManager;

    public delegate void LevelLoaded(int playerBlocks);
    public static event LevelLoaded levelLoaded;

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
        

        List<SaveData> objs = new List<SaveData>();

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

        
    }

    public void Load(string filename)
    {
        Debug.Log("Loading file at: " + Application.persistentDataPath + "/" + filename + ".dat");
        if (File.Exists(Application.persistentDataPath + "/" + filename + ".dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();

            FileStream file = File.Open(Application.persistentDataPath + "/" + filename + ".dat", FileMode.Open);

            List<SaveData> data = (List<SaveData>)bf.Deserialize(file);

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
            }
            file.Close();
        }
        
        if (levelLoaded != null)
        {
            blocksUsed = BlockManager.blockManager.GetList().Count;
            levelLoaded(blocksUsed);
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
