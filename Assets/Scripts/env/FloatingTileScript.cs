using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingTileScript : MonoBehaviour
{
    [SerializeField] float range, speed;

    private Vector3 initialPosition;
    private Vector3 currentPosition;

    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
        currentPosition = initialPosition;
    }

    // Update is called once per frame
    void Update()
    {
        float sinValue = Mathf.Sin(Time.time * speed);
        float targetX = (sinValue + 1) / 2 * range;

        currentPosition.x = targetX;
        transform.position = currentPosition + initialPosition;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && collision.gameObject.transform.position.y > transform.position.y)
        {
            collision.gameObject.GetComponent<Transform>().SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" )
        {
            collision.gameObject.GetComponent<Transform>().SetParent(null);
        }
    }
}
