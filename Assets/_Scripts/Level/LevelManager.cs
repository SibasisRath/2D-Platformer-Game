using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private static LevelManager instance;
    private const AllLevels defaultFirstLevel = AllLevels.Level1;
    private const AllLevels defaultLobbyLevel = AllLevels.Lobby;
    private const AllLevels defaultFinalLevel = AllLevels.Final;

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
        SetLevelStates(defaultLobbyLevel, LevelStates.Unlocked);
        SetLevelStates(defaultFirstLevel,LevelStates.Unlocked);
        SetLevelStates(defaultFinalLevel, LevelStates.Unlocked);
    }

    public void OnLevelCompletion(AllLevels nextLevelName)
    {
        SoundManager.Instance.Play(Sounds.LevelComplete);
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SetLevelStates((AllLevels)currentSceneIndex, LevelStates.Completed);
        SetLevelStates(nextLevelName, LevelStates.Unlocked);

    }
  
    public LevelStates GetLevelStates(AllLevels level) 
    {
        LevelStates levelStates = (LevelStates) PlayerPrefs.GetInt(level.ToString(), 0);
        return levelStates;
    }
    public void SetLevelStates(AllLevels level, LevelStates levelStates) 
    {
        PlayerPrefs.SetInt(level.ToString(), (int)levelStates);
        PlayerPrefs.Save();
    }
}
