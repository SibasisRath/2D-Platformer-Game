using UnityEngine;

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
