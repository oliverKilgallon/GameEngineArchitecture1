using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

enum MCFacehit
{
    Up,
    Down,
    Left,
    Right,
    North,
    South,
    None
}

[System.Serializable]
public class PlaceBlocks : MonoBehaviour
{

    public GameObject[] prefabs;
    public static int blockLimit = 12;

    public delegate void BlockChange(int blockNum);
    public static event BlockChange blockSwitch;

    public delegate void BlockSet(int amount);
    public static event BlockSet blockSet;

    private GameObject selectedPrefab;
    private int blocksPlaced;

    private void Awake()
    {
        SceneManager.sceneLoaded += LevelLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= LevelLoaded;
    }

    void Start ()
    {
        selectedPrefab = prefabs[0];
        blocksPlaced = 0;
	}
	
	void Update ()
    {
        if (PlayerController.isInEditor)
        {
            EditorControls();
        }
    }

    private void LevelLoaded(Scene scene, LoadSceneMode sceneMode)
    {

        if (scene.name == "Level1")
        {
            blockLimit = 12;
        }
        else
        {
            blockLimit = 20;
        }
    }

    void EditorControls()
    {
        if (Input.GetMouseButtonUp(0))
        {
            AddBlock();
        }
        else if (Input.GetMouseButtonUp(1))
        {
            RemoveBlock();
        }
        else if (Input.GetKeyUp("1"))
        {
            if (blockSwitch != null)
            {
                selectedPrefab = prefabs[0];
                blockSwitch(0);
            }
        }
        else if (Input.GetKeyUp("2"))
        {
            if (blockSwitch != null)
            {
                selectedPrefab = prefabs[1];
                blockSwitch(1);
            }
        }
        else if (Input.GetKeyUp("3"))
        {
            if (blockSwitch != null)
            {
                selectedPrefab = prefabs[2];
                blockSwitch(2);
            }
        }
    }

    void AddBlock()
    {
        if (blocksPlaced < blockLimit)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if (hit.transform != null && !hit.transform.CompareTag("Undeletable"))
                {
                    GameObject newObj = Instantiate(selectedPrefab, hit.transform.position + hit.normal, Quaternion.identity);
                    newObj.name = selectedPrefab.name;
                    BlockManager.blockManager.AddBlock(newObj);
                    blocksPlaced++;
                }
            }
            
            if (blockSet != null)
            {
                blockSet(blocksPlaced);
            }
        }
        else
        {
            Debug.Log("Limit reached!");
        }
    }

    void RemoveBlock()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            if (!hit.transform.gameObject.CompareTag("Undeletable") && !hit.transform.CompareTag("Node") && (blocksPlaced - 1) >= 0)
            {
                BlockManager.blockManager.RemoveBlock(hit.transform.gameObject.GetInstanceID());
                Destroy(hit.transform.gameObject);
                blocksPlaced--;
                if (blockSet != null)
                {
                    blockSet(blocksPlaced);
                }
            }
        }
    }
}
