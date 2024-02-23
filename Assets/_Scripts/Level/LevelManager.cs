using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private static LevelManager instance;
    private const string defaultFirstLevel = "Level1";

    public static LevelManager Instance { get{ return instance; } }

    private void Awake()
    {
        if (instance ==null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        setLevelStates(defaultFirstLevel,LevelStates.Unlocked);
    }

    public void OnLevelCompletion(string nextLevelName)
    {
        SoundManager.Instance.Play(Sounds.LevelComplete);
        Scene currentScene = SceneManager.GetActiveScene();
        setLevelStates(currentScene.name, LevelStates.Completed);
        setLevelStates(nextLevelName, LevelStates.Unlocked);

    }
  
    public LevelStates getLevelStates(string level) 
    {
        LevelStates levelStates = (LevelStates) PlayerPrefs.GetInt(level, 0);
        return levelStates;
    }
    public void setLevelStates(string level, LevelStates levelStates) 
    {
        if (level == "Final")
        {
            Debug.Log("Game ended.");
        }
        else
        {
            PlayerPrefs.SetInt(level, (int)levelStates);
            PlayerPrefs.Save();
            Debug.Log($"level status updated {level}, {levelStates}");
        }
        
    }
}
