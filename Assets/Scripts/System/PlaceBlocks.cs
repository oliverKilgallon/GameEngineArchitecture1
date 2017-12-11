using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

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
    
    public delegate void BlockEdit(int blockLimit);
    public static event BlockEdit blockEdit;

    //public delegate void BlockSet(int amount);
    //public static event BlockSet blockSet;

    private GameObject selectedPrefab;
    public static int blocksPlaced;
    
    void Start ()
    {
        selectedPrefab = prefabs[0];
        blocksPlaced = 0;
	}

    void Awake()
    {
        SceneManager.sceneLoaded += SetBlockLimit;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= SetBlockLimit;
    }

    void SetBlockLimit(Scene scene, LoadSceneMode scenemode)
    {
        int levelLimit = 0;

        //Set the amount of blocks the player can use based on the level
        switch (scene.buildIndex)
        {
            case 1:
                levelLimit = 12;
                break;
            default:
                levelLimit = 20;
                break;
        }

        //Sets the blocklimit to a previous count based on the blockmanager's count. If there is none, the default level limit is used.
        if (BlockManager.blockManager.GetList() != null)
        {
            blocksPlaced = BlockManager.blockManager.GetList().Count + 1;
        }

        //foreach (GameObject obj in BlockManager.blockManager.GetList())
        //{
        //    Debug.Log(obj.name);
        //}
        Debug.Log("Blocks placed is: " + blocksPlaced);
        blockLimit = levelLimit;
    }

	void Update ()
    {
        //Only allow the editor to be used if the player has enabled them
        if (PlayerController.isInEditor)
        {
            EditorControls();
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
        if (blocksPlaced < blockLimit && !EventSystem.current.IsPointerOverGameObject())
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
            if (blockEdit != null)
            {
                blockEdit(blocksPlaced);
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
            if (!hit.transform.gameObject.CompareTag("Undeletable") && !hit.transform.CompareTag("Node") && ((blocksPlaced - 1) >= 0) && !EventSystem.current.IsPointerOverGameObject())
            {
                BlockManager.blockManager.RemoveBlock(hit.transform.gameObject.GetInstanceID());
                Destroy(hit.transform.gameObject);
                blocksPlaced--;
                if (blockEdit != null)
                {
                    blockEdit(blocksPlaced);
                }
            }
            
        }
    }
}
