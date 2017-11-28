using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    private Color[] defBlockColours;

    public void Awake()
    {
        PlayerController.modeSwitch += isEditorOn;
        PlayerController.escapePressed += TogglePauseMenu;
        PlaceBlocks.blockSwitch += selectImage;
        PlaceBlocks.blockSet += SetBlockCount;
        SceneManager.sceneLoaded += LevelLoaded;
    }

    public void OnDisable()
    {
        PlayerController.modeSwitch -= isEditorOn;
        PlayerController.escapePressed -= TogglePauseMenu;
        PlaceBlocks.blockSwitch -= selectImage;
        PlaceBlocks.blockSet -= SetBlockCount;
        SceneManager.sceneLoaded -= LevelLoaded;
    }

    private void LevelLoaded(Scene scene, LoadSceneMode sceneMode)
    {
        maxPlayerBlockAmount.text = PlaceBlocks.blockLimit.ToString();
    }

    void Start ()
    {
        defBlockColours = new Color[blockImages.Length];
        for(int i = 0; i < blockImages.Length; i++)
        {
            defBlockColours[i] = blockImages[i].color;
        }
        currentlySelected = 0;
        playerBlockAmount = 0;
        blockImages[0].color = Color.red;
        maxPlayerBlockAmount.text = PlaceBlocks.blockLimit.ToString();
    }

    void SetBlockCount(int amount)
    {
        playerBlockAmount = amount;
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
        blockImages[currentlySelected].color = defBlockColours[currentlySelected];
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
        //GameManager.gameManager.SetDataString();
        GameManager.gameManager.Save(SceneManager.GetActiveScene().name.ToString());
        
    }
}
