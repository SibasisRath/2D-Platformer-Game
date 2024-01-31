using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLifeLineManager : MonoBehaviour
{
    [SerializeField] List<GameObject> lifelines;
    [SerializeField] GameObject gameOverScreen;

    private void Start()
    {
        gameOverScreen.SetActive(false);
    }

    public void UpdateLifeLine()
    {
        SoundManager.Instance.Play(Sounds.PlayerHurt);
        Destroy(lifelines[lifelines.Count - 1]);
        lifelines.RemoveAt(lifelines.Count - 1);
        transform.position = GetComponent<PlayerController>().LastCheckPoint.position;
        if (lifelines.Count <= 0)
        {
            SoundManager.Instance.Play(Sounds.PlayerDead);
            gameOverScreen.SetActive(true);
            transform.GetComponent<PlayerController>().enabled = false;
        }
        
    }

}
