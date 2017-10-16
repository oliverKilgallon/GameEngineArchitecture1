using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceBlocks : MonoBehaviour
{

    public GameObject prefab1;
    public GameObject prefab2;

    private GameObject selectedPrefab;

	void Start ()
    {
        selectedPrefab = prefab1;
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
            selectedPrefab = prefab1;
        }
        else if (Input.GetKeyUp("2"))
        {
            Debug.Log("Switching to block 2");
            selectedPrefab = prefab2;
        }
    }

    void AddBlock()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //Vector3 fwd = transform.TransformDirection(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            if (hit.transform != null)
            {
                Instantiate(selectedPrefab, hit.point, Quaternion.identity);
            }
        }
    }

    void RemoveBlock()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //Vector3 fwd = transform.TransformDirection(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            if (!hit.transform.gameObject.CompareTag("Undeletable"))
            {
                Destroy(hit.transform.gameObject);
            }
        }
    }
}
