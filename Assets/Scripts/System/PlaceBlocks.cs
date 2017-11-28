using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public GameObject playerBlockManager;

    public delegate void BlockChange(int blockNum);
    public static event BlockChange blockSwitch;

    public delegate void BlockAdd(int amount);
    public static event BlockAdd blockAdd;

    public delegate void BlockRemove(int amount);
    public static event BlockRemove blockRemove;

    private GameObject selectedPrefab;
    private int blocksPlaced;

    void Start ()
    {
        gameObject.SetActive(true);
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

    private void OnLevelWasLoaded(int level)
    {
        switch (level)
        {
            case 1:
                blockLimit = 12;
                break;
            default:
                blockLimit = 20;
                break;
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
                    newObj.transform.parent = playerBlockManager.transform;
                }
            }
            blocksPlaced++;
            if (blockAdd != null)
            {
                blockAdd(1);
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
                Destroy(hit.transform.gameObject);
                blocksPlaced--;
                if (blockRemove != null)
                {
                    blockRemove(-1);
                }
            }
        }
    }
}
