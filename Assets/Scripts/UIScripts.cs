using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UIScripts : MonoBehaviour
{
    public Text editorText;
    public PlayerController player;

    public void Awake()
    {
        PlayerController.modeSwitch += isEditorOn;
    }

    public void OnDisable()
    {
        PlayerController.modeSwitch -= isEditorOn;
    }
    void Start ()
    {

	}
	
	void Update ()
    {
	}

    void isEditorOn()
    {
        if (player.getIsInEditor().Equals(true))
        {
            editorText.text = "ON";
        }
        else
        {
            editorText.text = "OFF";
        }
    }
}
