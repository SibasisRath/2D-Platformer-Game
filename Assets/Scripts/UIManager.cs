using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Button nextLevelButton;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button nextLevelBackToMainButton;
    [SerializeField] private Button restartBackToMainButton;
    private void Start()
    {
        nextLevelButton.onClick.AddListener(() => {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            SoundManager.Instance.Play(Sounds.ButtonClick);
        });
        restartButton.onClick.AddListener(() => { 
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            SoundManager.Instance.Play(Sounds.ButtonClick);
        });
        nextLevelBackToMainButton.onClick.AddListener(() => {
            SceneManager.LoadScene("Lobby");
            SoundManager.Instance.Play(Sounds.ButtonClick);
        });
        restartBackToMainButton.onClick.AddListener(() => {
            SceneManager.LoadScene("Lobby");
            SoundManager.Instance.Play(Sounds.ButtonClick);
        });
    }
   
}
