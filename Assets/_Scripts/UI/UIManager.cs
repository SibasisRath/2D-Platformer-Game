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
    private int currentSceneIndex;
    private void Start()
    {
        SetUpButton(nextLevelButton, NextLevelButtonClicked);
        SetUpButton(restartButton, RestartButtonClicked);
        SetUpButton(nextLevelBackToMainButton, BackToMainButtonClicked);
        SetUpButton(restartBackToMainButton, BackToMainButtonClicked);

        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
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
    }

    private void NextLevelButtonClicked()
    {
        LevelLoader.LevelSelector((AllLevels)(currentSceneIndex+1));
    }

    private void RestartButtonClicked()
    {
        LevelLoader.LevelSelector((AllLevels)currentSceneIndex);
    }

    private void BackToMainButtonClicked()
    {
        LevelLoader.LevelSelector(AllLevels.Lobby);
    }
   
}
