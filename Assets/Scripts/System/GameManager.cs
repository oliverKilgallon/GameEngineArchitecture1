using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour {

    [SerializeField]
    public GameObject[] prefabs;

    public static GameManager gameManager;
    public int blocksUsed;
    public Transform[] blockTransforms;

    private string[] seperateObjs = { "|" };
    private string[] seperateData = { ";" };

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

    public void Save(string filename, string objectData, int objectAmount)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/" + filename + ".dat");

        List<SaveData> objs = new List<SaveData>();

        string[] objects = new string[objectAmount];

        //Seperate the data string into strings marking each object
        objects = objectData.Split(seperateObjs, System.StringSplitOptions.RemoveEmptyEntries);

        string objectDataString = "";

        //Re-combine the string into a continuous string of data seperated with ';' characters
        foreach (string str in objects)
        {
            objectDataString += str;
        }

        //Split the data once more so it is iterable
        objects = new string[objects.Length * 8];
        objects = objectDataString.Split(seperateData, System.StringSplitOptions.RemoveEmptyEntries);

        for (int i = 0; i < objectAmount * 8; i++)
        {
            SaveData data = new SaveData();
            if (i % 8 == 0 || i == 0)
            {
                data.objectIndex = System.Convert.ToInt32(objects[i]);
            }
            else if (i == 1 || i % 9 == 0)
            {
                data.position = new Vector3(
                    (float)System.Convert.ToDouble(objects[i]), 
                    (float)System.Convert.ToDouble(objects[i + 1]), 
                    (float)System.Convert.ToDouble(objects[i + 2])
                );
                i += 3;
            }
            else if (i % 4 == 0)
            {
                data.rotation = new Quaternion(
                    (float)System.Convert.ToDouble(objects[i]), 
                    (float)System.Convert.ToDouble(objects[i + 1]), 
                    (float)System.Convert.ToDouble(objects[i + 2]), 
                    (float)System.Convert.ToDouble(objects[i + 3])
                );
                i += 4;
            }
            objs.Add(data);
        }
        //{
        //    str.Split(seperateData, System.StringSplitOptions.RemoveEmptyEntries);
        //    SaveData data = new SaveData();
        //    data.objectIndex = System.Convert.ToInt32(objects[0]);
        //    data.position = objects[1];
        //    data.rotation = objects[2];
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
            foreach(SaveData sd in data)
            {
                Instantiate(prefabs[sd.objectIndex], sd.position, sd.rotation);
            }
            file.Close();
        }
    }
}

[System.Serializable]
class SaveData
{
    public int objectIndex;
    public Vector3 position;
    public Quaternion rotation;
}
