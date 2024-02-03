using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelFinish : MonoBehaviour
{
    [SerializeField] private GameObject levelCompleteScreen;
    [SerializeField] private string nextLevelName;
    [SerializeField] private GameObject successParticles;

    private void Start()
    {
        levelCompleteScreen.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController playerController = collision.GetComponent<PlayerController>();
        if (playerController != null) 
        {
            levelCompleteScreen.SetActive(true);
            playerController.enabled = false;
            Instantiate(successParticles,gameObject.transform);
            LevelManager.Instance.OnLevelCompletion(nextLevelName);

        }
    }
}
