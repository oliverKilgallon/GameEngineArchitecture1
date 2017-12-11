using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIScripts : MonoBehaviour
{
<<<<<<< HEAD
    public Text editorText;
    public Text blocksPlacedText;
    public Text blockLimitText;

    [SerializeField]
    public Image[] blockImages;

=======
    [SerializeField]
    public Image[] blockImages;

    public Text editorText;
    public Text currPlayerBlockAmount;
    public Text maxPlayerBlockAmount;
>>>>>>> a2bdaefb215ed6ce4a9bd7e9ce5f9cb73c8690d6
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
<<<<<<< HEAD
        PlaceBlocks.blockEdit += UpdatePlacedBlocks;

        SceneManager.sceneLoaded += UpdateBlockLimit;

        GameManager.levelLoaded += UpdatePlacedBlocks;
=======
        PlaceBlocks.blockSet += SetBlockCount;
        SceneManager.sceneLoaded += LevelLoaded;
>>>>>>> a2bdaefb215ed6ce4a9bd7e9ce5f9cb73c8690d6
    }

    public void OnDisable()
    {
        PlayerController.modeSwitch -= isEditorOn;
        PlayerController.escapePressed -= TogglePauseMenu;

        PlaceBlocks.blockSwitch -= selectImage;
<<<<<<< HEAD
        PlaceBlocks.blockEdit -= UpdatePlacedBlocks;

        SceneManager.sceneLoaded -= UpdateBlockLimit;

        GameManager.levelLoaded -= UpdatePlacedBlocks;
=======
        PlaceBlocks.blockSet -= SetBlockCount;
        SceneManager.sceneLoaded -= LevelLoaded;
>>>>>>> a2bdaefb215ed6ce4a9bd7e9ce5f9cb73c8690d6
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
<<<<<<< HEAD
=======
        //GameManager.gameManager.SetDataString();
>>>>>>> a2bdaefb215ed6ce4a9bd7e9ce5f9cb73c8690d6
        GameManager.gameManager.Save(SceneManager.GetActiveScene().name.ToString());
        
    }

    public void Load()
    {
        GameManager.gameManager.Load(SceneManager.GetActiveScene().name.ToString());
    }
}
