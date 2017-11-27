using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager gameManager;
    public int blocksUsed;
    public Transform[] blockTransforms;
    private string[] seperatingChars = {";", "|"};
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

    public void Save(string filename, string objectData)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/" + filename + ".dat");

        List<SaveData> objs = new List<SaveData>();

        string[] objects = new string[BlockManager.blockManager.GetList().Count + 1];

        objects = objectData.Split(seperatingChars, System.StringSplitOptions.RemoveEmptyEntries);

        foreach(string str in objects)
        {
            str.Split();
            SaveData data = new SaveData();
            data.objectName = objects[0];
            //data.position = objects[1];
            //data.rotation = objects[2];
            objs.Add(data);
        }
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
            foreach(SaveData sd in data)
            {
                Instantiate(Resources.Load("Prefabs/Placeables/" + sd.objectName , typeof(GameObject)), sd.position, sd.rotation);
            }
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

            dataString += go.transform.localScale.x + ";";
            dataString += go.transform.localScale.y + ";";
            dataString += go.transform.localScale.z + ";";
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
