using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeadZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerLifeLineManager playerLifeLineManager = collision.GetComponent<PlayerLifeLineManager>();

        if (playerLifeLineManager != null)
        {
            playerLifeLineManager.UpdateLifeLine();
        }
    }
}
