using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    string currentScene;

    // Start is called before the first frame update
    void Start()
    {
        currentScene = SceneManager.GetActiveScene().name;
    }

    public void OnRestart() {
        SoundManager.Instance.Play(Sounds.ButtonClick);
        SceneManager.LoadScene(currentScene);
    }

    public static void LevelSelector(string level)
    {

        LevelStates levelStates = LevelManager.Instance.getLevelStates(level);
        switch (levelStates)
        {
            case LevelStates.Completed:
                
            case LevelStates.Unlocked:
                SoundManager.Instance.Play(Sounds.ButtonClick);
                SceneManager.LoadScene(level);
                break;
            case LevelStates.Locked:
                Debug.Log("This is locked.");
                SoundManager.Instance.Play(Sounds.LevelLocked);
                break;
            default:
                Debug.Log("Something went worng.");
                break;
        }
       
    }

    public void OnQuit()
    {
        Debug.Log("quit");
        Application.Quit();
    }
}
