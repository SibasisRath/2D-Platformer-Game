using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] int currentScene;
    [SerializeField] TextMeshPro instruction;

    private bool _isLevelComplete;

    // Start is called before the first frame update
    void Start()
    {
        currentScene = SceneManager.GetActiveScene().buildIndex;
        instruction.enabled = false;
        _isLevelComplete = false;
    }

    private void Update()
    {
        if (_isLevelComplete)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                OnRestart();
            }
            if (Input.GetKeyDown(KeyCode.N))
            {
                NextLevel();
            }
        }
    }

    public void OnRestart() { 
        SceneManager.LoadScene(currentScene);
    }

    public void NextLevel()
    {
        try
        {
            SceneManager.LoadScene(currentScene + 1);
        }
        catch(IndexOutOfRangeException e) { 
            Debug.Log("there is no next level.");
            Debug.Log(e);
        }
        finally { }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            _isLevelComplete = true;
            instruction.enabled = true;
        }

    }
}