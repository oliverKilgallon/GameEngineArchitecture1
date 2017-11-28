using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UIScripts : MonoBehaviour
{
    [SerializeField]
    public Image[] blockImages;

    public Text editorText;
    public Text currPlayerBlockAmount;
    public Text maxPlayerBlockAmount;
    public PlayerController player;
    public GameObject blockPanel;
    public GameObject pausePanel;

    private int currentlySelected;
    private int playerBlockAmount;

    public void Awake()
    {
        PlayerController.modeSwitch += isEditorOn;
        PlayerController.escapePressed += TogglePauseMenu;
        PlaceBlocks.blockSwitch += selectImage;
        PlaceBlocks.blockAdd += SetBlockCount;
        PlaceBlocks.blockRemove += SetBlockCount;
    }

    public void OnDisable()
    {
        PlayerController.modeSwitch -= isEditorOn;
        PlayerController.escapePressed -= TogglePauseMenu;
        PlaceBlocks.blockSwitch -= selectImage;
        PlaceBlocks.blockAdd -= SetBlockCount;
        PlaceBlocks.blockRemove += SetBlockCount;
    }

    private void OnLevelWasLoaded(int level)
    {
        maxPlayerBlockAmount.text = PlaceBlocks.blockLimit.ToString();
    }

    void Start ()
    {
        currentlySelected = 0;
        playerBlockAmount = 0;
        blockImages[0].color = Color.red;
        maxPlayerBlockAmount.text = PlaceBlocks.blockLimit.ToString();
    }

    void SetBlockCount(int amount)
    {
        playerBlockAmount += amount;
        currPlayerBlockAmount.text = playerBlockAmount.ToString();
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
