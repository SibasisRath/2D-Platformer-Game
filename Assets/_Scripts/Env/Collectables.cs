using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Collectables : MonoBehaviour
{
    [SerializeField] private int value = 0;
    [SerializeField] private bool _isCaptured = false;

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
            Debug.Log($"Player collected {gameObject.name}");
            SoundManager.Instance.Play(Sounds.Collection);
            playerController.PickUpCollectables(value);
            _isCaptured = true;
            boxCollider.enabled = false;
            animator.SetBool("Captured", _isCaptured);
            transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, transform.position.y + collectableVerticalDisplacement, transform.position.z), animationSpeed * Time.deltaTime);
            Invoke("disablingGameObject", gameObjectDisablingTime);
            
        }
    }

    void disablingGameObject()
    {
        gameObject.SetActive(false);
    }
}
