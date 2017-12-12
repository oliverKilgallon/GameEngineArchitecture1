using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIScripts : MonoBehaviour
{
    public delegate void NewGame();
    public static event NewGame newGameEvent;

    public Text editorText;
    public Text blocksPlacedText;
    public Text blockLimitText;

    [SerializeField]
    public Image[] blockImages;
    
    //public Text currPlayerBlockAmount;
    //public Text maxPlayerBlockAmount;
    public PlayerController player;

    public GameObject blockPanel;
    public GameObject pausePanel;
    public GameObject loadSavePanel;

    public InputField LoadInputField;

    private int currentlySelected;
    private int playerBlockAmount;
    private Color[] defBlockColours;

    public void Awake()
    {
        PlayerController.modeSwitch += isEditorOn;
        PlayerController.escapePressed += TogglePauseMenu;

        PlaceBlocks.blockSwitch += selectImage;
        PlaceBlocks.blockEdit += UpdatePlacedBlocks;

        SceneManager.sceneLoaded += LevelLoaded;

        GameManager.levelLoaded += UpdatePlacedBlocks;
    }

    public void OnDisable()
    {
        PlayerController.modeSwitch -= isEditorOn;
        PlayerController.escapePressed -= TogglePauseMenu;

        PlaceBlocks.blockSwitch -= selectImage;
        PlaceBlocks.blockEdit -= UpdatePlacedBlocks;

        GameManager.levelLoaded -= UpdatePlacedBlocks;
        SceneManager.sceneLoaded -= LevelLoaded;
    }

    //Set the maximum placeable blocks text to the blocklimit in the PlaceBlocksScript
    private void LevelLoaded(Scene scene, LoadSceneMode sceneMode)
    {
        if (scene.name != "MainMenu")
        {
            blockLimitText.text = PlaceBlocks.blockLimit.ToString();
            blocksPlacedText.text = PlaceBlocks.blocksPlaced.ToString();
        }
        if(pausePanel != null) pausePanel.SetActive(false);
    }

    public void StartNewGame()
    {
        SceneManager.LoadScene(1);
        if (newGameEvent != null)
        {
            
            newGameEvent();
        }
    }

    //Setup the block editor ui
    void Start ()
    {
        if (SceneManager.GetActiveScene().name != "MainMenu")
        {
            defBlockColours = new Color[blockImages.Length];
            for (int i = 0; i < blockImages.Length; i++)
            {
                defBlockColours[i] = blockImages[i].color;
            }
            currentlySelected = 0;
            playerBlockAmount = 0;
            blockImages[0].color = Color.red;
            blockLimitText.text = PlaceBlocks.blockLimit.ToString();
        }
    }

    //Called if the load button on the main menu is pressed
    public void MainLoadPressed()
    {
        GameManager.gameManager.Load(LoadInputField.text);
        SceneManager.sceneLoaded += BlockManager.Load;
    }

    //Set the saveLoad panel to active if "load" is pressed in the main menu
    public void OpenLoadSaveUI()
    {
        loadSavePanel.SetActive(true);
    }

    //Set the saveLoad panel to inactive if "exit" is pressed in the main menu
    public void ExitLoadSaveUI()
    {
        loadSavePanel.SetActive(false);
    }

    //Toggles the in-game block panel based on whether the player is in the editor or not
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

    //Sets the blockLimit text based on which scene has currently loaded in
    void UpdateBlockLimit(Scene scene, LoadSceneMode sceneMode)
    {
        switch (scene.buildIndex)
        {
            case 0:
                break;
            case 1:
                blockLimitText.text = 12.ToString();
                break;
            default:
                blockLimitText.text = 20.ToString();
                break;
        }
    }

    //Set the player's placed blocks text value to the passed in value
    void UpdatePlacedBlocks(int newLimit)
    {
        blocksPlacedText.text = newLimit.ToString();
    }

    //Highlight the next image in the blockPanel UI
    void selectImage(int blockNum)
    {
        blockImages[currentlySelected].color = defBlockColours[currentlySelected];
        currentlySelected = blockNum;
        blockImages[currentlySelected].color = Color.red;
    }

    //Sets the pausePanel to active or inactive based on which state it is currently in
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

    //Loads the MainMenu scene
    public void ReturnToMain()
    {
        SceneManager.LoadScene("MainMenu");
    }

    //Call the gamemanager's save method and pass in the current scenes name as a string
    public void Save()
    {
        GameManager.gameManager.Save(SceneManager.GetActiveScene().name.ToString());
    }

    //Call the gamemanager's load method and pass in the current scenes name as a string
    public void Load()
    {
        GameManager.gameManager.Load(SceneManager.GetActiveScene().name.ToString());
    }
}
