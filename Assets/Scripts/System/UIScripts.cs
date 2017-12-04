using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIScripts : MonoBehaviour
{
    public Text editorText;
    public Text blocksPlacedText;
    public Text blockLimitText;

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
        PlaceBlocks.blockEdit += UpdatePlacedBlocks;

        SceneManager.sceneLoaded += UpdateBlockLimit;

        GameManager.levelLoaded += UpdatePlacedBlocks;
    }

    public void OnDisable()
    {
        PlayerController.modeSwitch -= isEditorOn;
        PlayerController.escapePressed -= TogglePauseMenu;

        PlaceBlocks.blockSwitch -= selectImage;
        PlaceBlocks.blockEdit -= UpdatePlacedBlocks;

        SceneManager.sceneLoaded -= UpdateBlockLimit;

        GameManager.levelLoaded -= UpdatePlacedBlocks;
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

    void UpdateBlockLimit(Scene scene, LoadSceneMode sceneMode)
    {
        switch (scene.buildIndex)
        {
            case 1:
                blockLimitText.text = 12.ToString();
                break;
            default:
                blockLimitText.text = 20.ToString();
                break;
        }
    }
    void UpdatePlacedBlocks(int newLimit)
    {
        blocksPlacedText.text = newLimit.ToString();
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
        SceneManager.LoadScene("MainMenu");
    }

    public void Save()
    {
        GameManager.gameManager.Save(SceneManager.GetActiveScene().name.ToString());
        
    }

    public void Load()
    {
        GameManager.gameManager.Load(SceneManager.GetActiveScene().name.ToString());
    }
}
