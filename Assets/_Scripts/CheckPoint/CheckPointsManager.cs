using UnityEngine;

public class CheckPointsManager : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
        if (playerController != null)
        {
            playerController.LastCheckPoint = transform;
        }
    }
}
