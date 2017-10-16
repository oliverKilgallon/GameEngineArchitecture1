using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject editor;
    public bool isInEditor = false;
    public float speed;
    public float rotSpeed;
    public float jumpForce;

    public delegate void ModeChange();
    public static event ModeChange modeSwitch;
	
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
        }
        if (Input.GetKeyUp("space"))
        {
            gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0, jumpForce, 0));
        }
        if (Input.GetKeyUp("r"))
        {
            gameObject.GetComponent<Movement>().ResetRot();
        }
    }

    private void EditorCheck()
    {
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
}
