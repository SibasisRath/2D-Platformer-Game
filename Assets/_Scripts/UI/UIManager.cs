using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Button nextLevelButton;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button nextLevelBackToMainButton;
    [SerializeField] private Button restartBackToMainButton;
    private void Start()
    {
        SetUpButton(nextLevelButton, NextLevelButtonClicked);
        SetUpButton(restartButton, RestartButtonClicked);
        SetUpButton(nextLevelBackToMainButton, BackToMainButtonClicked);
        SetUpButton(restartBackToMainButton, BackToMainButtonClicked);

    }

    private void SetUpButton(Button button, UnityAction unityAction)
    {
        if (button!=null)
        {
            button.onClick.AddListener(() =>{
                SoundManager.Instance.Play(Sounds.ButtonClick);
                unityAction?.Invoke();
            });
        }
        else
        {
            Debug.Log($"{button} is null.");
        }
    }

    private void NextLevelButtonClicked()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void RestartButtonClicked()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void BackToMainButtonClicked()
    {
        SceneManager.LoadScene("Lobby");
    }
   
}
