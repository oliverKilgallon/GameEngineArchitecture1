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
    public GameObject pausePanel;

    private int currentlySelected;

    public void Awake()
    {
        PlayerController.modeSwitch += isEditorOn;
        PlayerController.escapePressed += TogglePauseMenu;
        PlaceBlocks.blockSwitch += selectImage;
    }

    public void OnDisable()
    {
        PlayerController.modeSwitch -= isEditorOn;
        PlayerController.escapePressed -= TogglePauseMenu;
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

    public void TogglePauseMenu()
    {
        if (pausePanel.activeSelf)
        {
            pausePanel.SetActive(false);
        }
        else
        {
            pausePanel.SetActive(true);
        }
    }

    public void ReturnToMain()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }

    public void Save()
    {
        GameManager.gameManager.SetDataString();
        GameManager.gameManager.Save(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name.ToString(), GameManager.gameManager.GetDataString());
        
    }
}
