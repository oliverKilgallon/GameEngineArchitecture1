using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject editor;
    public static bool isInEditor = false;
    public float jumpForce;

    public delegate void ModeChange();
    public static event ModeChange modeSwitch;

    public delegate void EscapePressed();
    public static event EscapePressed escapePressed;
	
	void Update ()
    {
        ProcessInput();

        EditorCheck();
    }

    public bool getIsInEditor()
    {
        return isInEditor;
    }

    private void ProcessInput()
    {
        if (Input.GetKeyUp("f"))
        {
            isInEditor = !isInEditor;
            if (modeSwitch != null)
            {
                modeSwitch();
            }
            if (isInEditor)
            {
                editor.SetActive(true);
            }
            else
            {
                editor.SetActive(false);
            }
        }

        if (Input.GetKeyUp("space"))
        {
            gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0, jumpForce, 0));
        }

        if (Input.GetKeyUp("escape"))
        {
            if (escapePressed != null)
            {
                escapePressed();
            }
        }
    }

    private void EditorCheck()
    {
        //if (isInEditor.Equals(false))
        //{
        //    gameObject.GetComponent<Movement>().Move();
        //    gameObject.GetComponent<Rigidbody>().isKinematic = false;
        //}
        //else if (isInEditor.Equals(true))
        //{
        //    gameObject.GetComponent<Rigidbody>().isKinematic = true;
        //}
        if (isInEditor)
        {
            gameObject.GetComponent<Rigidbody>().isKinematic = true;
        }
        else
        {
            gameObject.GetComponent<Movement>().Move();
            gameObject.GetComponent<Rigidbody>().isKinematic = false;
        }
    }
}
