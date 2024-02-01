using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Collectables : MonoBehaviour
{
    [SerializeField] int value = 0;
    [SerializeField] bool _isCaptured = false;

    private void Start()
    {
        gameObject.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") )
        {
            Debug.Log($"Player collected {gameObject.name}");
            SoundManager.Instance.Play(Sounds.Collection);
            collision.gameObject.GetComponent<PlayerController>().PickUpCollectables(value);
            _isCaptured = true;
            GetComponent<BoxCollider2D>().enabled = false;
            gameObject.GetComponent<Animator>().SetBool("Captured", _isCaptured);
            transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, transform.position.y + 3f, transform.position.z), 4 * Time.deltaTime);
            Invoke("disablingGameObject", 1.5f);
            
        }
    }

    void disablingGameObject()
    {
        gameObject.SetActive(false);
    }
}
