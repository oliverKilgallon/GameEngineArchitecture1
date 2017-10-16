using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject editor;
    public bool isInEditor;
    public float speed;
    public float rotSpeed;

    public delegate void ModeChange();
    public static event ModeChange modeSwitch;

	void Start ()
    {
        isInEditor = false;
	}
	
	
	void Update ()
    {
        if (Input.GetKeyUp("f"))
        {
            isInEditor = !isInEditor;
            if (modeSwitch != null)
            {
                modeSwitch();
            }
        }

        if (isInEditor.Equals(false))
        {
            gameObject.GetComponent<Movement>().Move();
            editor.SetActive(false);
        }
        else if (isInEditor.Equals(true))
        {
            editor.SetActive(true);
        }
    }

    public bool getIsInEditor()
    {
        return isInEditor;
    }
}
