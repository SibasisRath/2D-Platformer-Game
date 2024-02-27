using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float speed = 4f;
    private float raycastGroundDistance = 0.2f;
    [SerializeField] private GameObject enemyGroundCheckObject;
    [SerializeField] private LayerMask groundLayer;
    private bool isMovingRight = true; // Track the direction of movement


    private const int flipDegree = 180;

    void Update()
    {
        // Cast a ray downwards to check for ground
        RaycastHit2D groundHit = Physics2D.Raycast(enemyGroundCheckObject.transform.position, Vector2.down, raycastGroundDistance, groundLayer);

        // Move the enemy horizontally
        Vector3 movement = transform.position;
        movement.x += (isMovingRight ? 1 : -1) * speed * Time.deltaTime;
        transform.position = movement;

        // Check if there's no ground beneath
        if (!groundHit)
        {
            // Flip the enemy
            Flip();
        }
    }

    private void Flip()
    {
        // Flip the enemy sprite horizontally
        if (isMovingRight)
        {   
            transform.rotation = Quaternion.Euler(transform.rotation.x, flipDegree, transform.rotation.z);
        }
        else
        {
            transform.rotation = Quaternion.Euler(transform.rotation.x, 0, transform.rotation.z);
        }
        isMovingRight = !isMovingRight;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerLifeLineManager playerLifeLineManager = collision.gameObject.GetComponent<PlayerLifeLineManager>();

        if (playerLifeLineManager != null)
        {
            playerLifeLineManager.UpdateLifeLine();
        }
        else
        {
            Flip();
        }
    }
}
