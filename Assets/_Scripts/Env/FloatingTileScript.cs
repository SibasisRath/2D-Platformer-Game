using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingTileScript : MonoBehaviour
{
    [SerializeField] private float range; 
    [SerializeField] private float speed;

    private Vector3 initialPosition;
    private Vector3 currentPosition;

    //In this script there is a sin function whose range is -1 to 1. I am trying to make it from 0 to 1
    private const int addingConstant = 1;
    private const int dividingConstant = 2;

    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float sinValue = Mathf.Sin(Time.time * speed);
        float targetX = (sinValue + addingConstant) / dividingConstant * range;

        currentPosition = initialPosition;
        currentPosition.x += targetX;
        transform.position = currentPosition;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Transform collidedObject = collision.gameObject.transform;
        if (collidedObject.position.y > transform.position.y)
        {
            collidedObject.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {   
        collision.gameObject.transform.SetParent(null);
    }
}
