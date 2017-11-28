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
<<<<<<< HEAD

    private string[] seperateObjs = { "|" };
    private string[] seperateData = { ";" };
=======
    private string[] seperatingChars = {";", "|"};
    private string dataString;
>>>>>>> dd549ed5eaa0c28eedebefa491111d76404f26d0

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

<<<<<<< HEAD
        string[] objects = new string[objectAmount];

        //Seperate the data string into strings marking each object
        objects = objectData.Split(seperateObjs, System.StringSplitOptions.RemoveEmptyEntries);

        string objectDataString = "";
=======
        string[] objects = new string[BlockManager.blockManager.GetList().Count + 1];
>>>>>>> dd549ed5eaa0c28eedebefa491111d76404f26d0

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
<<<<<<< HEAD
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
=======
            data.objectName = objects[0];
            //data.position = objects[1];
            //data.rotation = objects[2];
>>>>>>> dd549ed5eaa0c28eedebefa491111d76404f26d0
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
<<<<<<< HEAD
                Instantiate(prefabs[sd.objectIndex], sd.position, sd.rotation);
=======
                Instantiate(Resources.Load("Prefabs/Placeables/" + sd.objectName , typeof(GameObject)), sd.position, sd.rotation);
>>>>>>> dd549ed5eaa0c28eedebefa491111d76404f26d0
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
    public int objectIndex;
    public Vector3 position;
    public Quaternion rotation;
<<<<<<< HEAD
=======
    public string objectName;
>>>>>>> dd549ed5eaa0c28eedebefa491111d76404f26d0
}
