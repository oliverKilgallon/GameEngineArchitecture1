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
    public int blockLimit = 12;
    public GameObject playerBlockManager;

    public delegate void BlockChange(int blockNum);
    public static event BlockChange blockSwitch;

    private GameObject selectedPrefab;
    private int blocksPlaced;

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
            if (!hit.transform.gameObject.CompareTag("Undeletable") && !hit.transform.CompareTag("Node"))
            {
                Destroy(hit.transform.gameObject);
            }
        }
        if ((blocksPlaced - 1) > 0)
        {
            blocksPlaced--;
        }
    }

    //Vector3 ClampPos(Vector3 position, float clampIncrement)
    //{
    //    return new Vector3 (
    //        Mathf.Floor(position.x / clampIncrement) * clampIncrement,
    //        Mathf.Floor(position.y / clampIncrement) * clampIncrement,
    //        Mathf.Floor(position.z / clampIncrement) * clampIncrement
    //        );
    //}

    private Vector3 getDisplacement(MCFacehit facehit)
    {
        switch (facehit)
        {
            case MCFacehit.Up:
                return new Vector3(0, 1, 0);
            case MCFacehit.Down:
                return new Vector3(0, -1, 0);
            case MCFacehit.North:
                return new Vector3(0, 0, 1);
            case MCFacehit.South:
                return new Vector3(0, 0, -1);
            case MCFacehit.Right:
                return new Vector3(1, 0, 0);
            case MCFacehit.Left:
                return new Vector3(-1, 0, 0);
            default:
                return new Vector3(0, 0, 0);
        }
    }
    private MCFacehit getFaceClicked(RaycastHit hit)
    {
        Vector3 facehit = hit.normal - Vector3.up;

        if (facehit == new Vector3(0, 0, 0))
        {
            return MCFacehit.Up;
        }
        else if (facehit == new Vector3(0, -1, 0))
        {
            return MCFacehit.Down;
        }
        else if (facehit == new Vector3(0, -1, 1))
        {
            return MCFacehit.North;
        }
        else if (facehit == new Vector3(0, -1, -1))
        {
            return MCFacehit.South;
        }
        else if (facehit == new Vector3(1, -1, 0))
        {
            return MCFacehit.Right;
        }
        else if (facehit == new Vector3(-1, -1, 0))
        {
            return MCFacehit.Left;
        }

        return MCFacehit.None;
    }
}
