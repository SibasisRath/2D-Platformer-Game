using UnityEngine;


public class Collectables : MonoBehaviour
{
    [SerializeField] private int value = 0;
    [SerializeField] private bool isCaptured = false;

    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private Animator animator;

    private float gameObjectDisablingTime = 1.5f;

    //These below numbers are responsible for collectable animation
    private const float collectableVerticalDisplacement = 3f;
    private const float animationSpeed = 4f;

    private void Start()
    {
        gameObject.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
        if (playerController != null )
        {
            SoundManager.Instance.Play(Sounds.Collection);
            playerController.PickUpCollectables(value);

            isCaptured = true;
            boxCollider.enabled = false;

            animator.SetBool("Captured", isCaptured);

            transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, transform.position.y + collectableVerticalDisplacement, transform.position.z), animationSpeed * Time.deltaTime);

            Invoke(nameof(disablingGameObject), gameObjectDisablingTime);
            
        }
    }

    private void disablingGameObject()
    {
        gameObject.SetActive(false);
    }
}
