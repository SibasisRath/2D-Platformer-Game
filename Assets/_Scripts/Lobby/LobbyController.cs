using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class LobbyController : MonoBehaviour
{
    [SerializeField] private GameObject levelSelection;
    [SerializeField] private Button buttonPlay;
    [SerializeField] private Button buttonQuit;
    [SerializeField] private Button buttonMain;

    [SerializeField] private Button[] levelButtons;

    private GameObject levelManager;

    private void Awake()
    {
        levelSelection.SetActive(false);
        buttonPlay.onClick.AddListener(()=> { 
            SoundManager.Instance.Play(Sounds.ButtonClick);
            this.PlayGame();
        });
        buttonQuit.onClick.AddListener(()=> {
            SoundManager.Instance.Play(Sounds.ButtonClick);
            this.QuitGame();
        });
        buttonMain.onClick.AddListener(() => {
            SoundManager.Instance.Play(Sounds.ButtonClick);
            levelSelection.SetActive(false);
        });
    }
    private void Start()
    {
        levelManager = GameObject.Find("LevelManager");

        foreach (var lButton in levelButtons)
        {
            lButton.onClick.AddListener(() => {
                LevelLoader.LevelSelector(lButton.gameObject.name);
                SoundManager.Instance.Play(Sounds.ButtonClick);
            });

           /* lButton.interactable = false;
            if (lButton.gameObject.name == "Level1")
            {
                lButton.interactable = true;
            }

            else
            {
                if ((int)LevelManager.Instance.getLevelStatus(lButton.gameObject.name) != 0)
                {
                    lButton.interactable = false;
                }
                else { lButton.interactable = true; }
            }
           */
        }
    }

    private void QuitGame()
    {
        Application.Quit();
    }

    private void PlayGame()
    {
        //Level Selection is a parent gameobject acting as a pop-up menu for selecting levels
        //Buttons for the levels are child of Level Selection.
        levelSelection.SetActive(true);
    }
}
