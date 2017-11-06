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
    public GameObject PlayerBlockManager;

    private GameObject selectedPrefab;
    private GameObject[] blocksPlaced;

	void Start ()
    {
        selectedPrefab = prefabs[0];
	}
	

	void Update ()
    {
        EditorControls();
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
            Debug.Log("Switching to block 1");
            selectedPrefab = prefabs[0];
        }
        else if (Input.GetKeyUp("2"))
        {
            Debug.Log("Switching to block 2");
            selectedPrefab = prefabs[1];
        }
        else if (Input.GetKeyUp("3"))
        {
            Debug.Log("Switching to block 3");
            selectedPrefab = prefabs[2];
        }
    }

    void AddBlock()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            if (hit.transform != null && !hit.transform.CompareTag("Undeletable"))
            {
                GameObject newObj = Instantiate(selectedPrefab, hit.transform.position + getDisplacement(getFaceClicked(hit)), Quaternion.identity);
                newObj.transform.parent = PlayerBlockManager.transform;
            }
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
