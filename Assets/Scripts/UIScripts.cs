using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UIScripts : MonoBehaviour
{
    public Text editorText;
    [SerializeField]
    public Image[] blockImages;
    public PlayerController player;
    public GameObject blockPanel;

    private int currentlySelected;

    public void Awake()
    {
        PlayerController.modeSwitch += isEditorOn;
        PlaceBlocks.blockSwitch += selectImage;
    }

    public void OnDisable()
    {
        PlayerController.modeSwitch -= isEditorOn;
        PlaceBlocks.blockSwitch -= selectImage;
    }
    void Start ()
    {
        currentlySelected = 0;
        blockImages[0].color = Color.red;
	}

    void isEditorOn()
    {
        if (PlayerController.isInEditor)
        {
            editorText.text = "Editor";
            blockPanel.SetActive(true);
        }
        else
        {
            editorText.text = "Game";
            blockPanel.SetActive(false);
        }
    }

    void selectImage(int blockNum)
    {
        blockImages[currentlySelected].color = Color.white;
        currentlySelected = blockNum;
        blockImages[currentlySelected].color = Color.red;
    }
}
