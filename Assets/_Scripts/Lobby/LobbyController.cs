using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

[RequireComponent(typeof(Button))]
public class LobbyController : MonoBehaviour
{
    [SerializeField] private GameObject levelSelection;
    [SerializeField] private Button buttonPlay;
    [SerializeField] private Button buttonQuit;
    [SerializeField] private Button buttonMain;

    [SerializeField] private Button[] levelButtons;

    private void Awake()
    {
        SetupButton(buttonPlay, PlayButtonClick);
        SetupButton(buttonQuit, QuitButtonClick);
        SetupButton(buttonMain, MainButtonClick);
    }
    private void Start()
    {
        for (int i = 0; i < levelButtons.Length; i++)
        {
            int index = i;
            AllLevels status = (AllLevels)Enum.Parse(typeof(AllLevels), levelButtons[index].gameObject.name);
            SetupButton(levelButtons[i], () => LevelButtonClick(status));
        }
    }

    private void SetupButton(Button button, UnityAction action)
    {
        if (button != null)
        {
            button.onClick.AddListener(() =>
            {
                action?.Invoke();
            });
        }
    }

    private void QuitButtonClick()
    {
        SoundManager.Instance.Play(Sounds.ButtonClick);
        Application.Quit();
   
    }

    private void PlayButtonClick()
    {
        SoundManager.Instance.Play(Sounds.ButtonClick);
        levelSelection.SetActive(true);
    }

    private void MainButtonClick()
    {
        SoundManager.Instance.Play(Sounds.ButtonClick);
        levelSelection.SetActive(false);
    }

    private void LevelButtonClick(AllLevels levelName)
    {
        LevelLoader.LevelSelector(levelName);
    }
}
