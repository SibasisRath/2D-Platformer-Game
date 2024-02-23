using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLifeLineManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> lifelines;
    [SerializeField] private GameObject gameOverScreen;

    [SerializeField] private ParticleSystem deadParticles;

    [SerializeField] private PlayerController playerController;
    [SerializeField] private Animator animator;

    private void Start()
    {
        gameOverScreen.SetActive(false);
    }

    public void UpdateLifeLine()
    {
        SoundManager.Instance.Play(Sounds.PlayerHurt);
        Destroy(lifelines[lifelines.Count - 1]);
        lifelines.RemoveAt(lifelines.Count - 1);
        transform.position = playerController.LastCheckPoint.position;
        animator.SetTrigger("Hurt");

        if (lifelines.Count <= 0)
        {
            SoundManager.Instance.Play(Sounds.PlayerDead);
            deadParticles.Play();
            animator.SetTrigger("Death");
            gameOverScreen.SetActive(true);
            playerController.enabled = false;
        }

       
    }

}
