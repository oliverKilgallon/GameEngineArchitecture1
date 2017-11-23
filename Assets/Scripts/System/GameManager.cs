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

        string[] objects = new string[5];

        objects = objectData.Split(seperatingChars, System.StringSplitOptions.RemoveEmptyEntries);

        foreach(string str in objects)
        {
            str.Split();
            SaveData data = new SaveData();
            data.objectIndex = System.Convert.ToInt32(objects[0]);
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
                //Instantiate(prefab[sd.objectIndex], sd.position, sd.rotation);
            }
            file.Close();
        }
    }
}

[System.Serializable]
class SaveData
{
    public Vector3 position;
    public Quaternion rotation;
    public int objectIndex;
    public Transform[] blockTransforms;
}
