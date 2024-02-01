using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] float cameraStableLeftRange;
    [SerializeField] float cameraStableRightRange;
    [SerializeField] float cameraStableUpRange;
    [SerializeField] float cameraStableDownRange;
    [SerializeField] float cameraFollowSpeed;
    [SerializeField] float cameraAdjustingSpeed;

    [SerializeField] float cameraOffset;

    Vector3 cameraPosition;

    PlayerController player;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        if (player == null) { return; }

        transform.SetParent(player.transform);
        cameraPosition = transform.position;
        UpdatingValues();
    }

    private void UpdatingValues()
    {
        cameraStableLeftRange += transform.position.x;
        cameraStableRightRange += transform.position.x;
        cameraStableDownRange += transform.position.y;
        cameraStableUpRange += transform.position.y;
    }

    // Update is called once per frame
    void LateUpdate()
    {

        if (player != null)
        {
            float target;
            if (player.transform.position.x <= cameraStableLeftRange)
            {
                target = player.transform.position.x - cameraOffset;
                cameraPosition.x = Mathf.Lerp(cameraPosition.x, target, Time.deltaTime * cameraFollowSpeed);
            }

            if (player.transform.position.y <= cameraStableDownRange)
            {
                target = player.transform.position.y - cameraOffset;
                cameraPosition.y = Mathf.Lerp(cameraPosition.y, target, Time.deltaTime * cameraFollowSpeed);
            }

            else
            {
                if (player.transform.position.x >= cameraStableRightRange)
                {
                    target = player.transform.position.x + cameraOffset;
                    cameraPosition.x = Mathf.Lerp(cameraPosition.x, target, Time.deltaTime * cameraFollowSpeed);
                }
                if (player.transform.position.y >= cameraStableUpRange)
                {
                    target = player.transform.position.y + cameraOffset;
                    cameraPosition.y = Mathf.Lerp(cameraPosition.y, target, Time.deltaTime * cameraFollowSpeed);
                }
                else
                {
                    float targetX = player.transform.position.x + cameraOffset;
                    float targetY = player.transform.position.y + cameraOffset;
                    cameraPosition.x = Mathf.Lerp(cameraPosition.x, targetX, Time.deltaTime * cameraAdjustingSpeed);
                    cameraPosition.y = Mathf.Lerp(cameraPosition.y, targetY, Time.deltaTime * cameraAdjustingSpeed);
                    UpdatingValues();
                }
            }

           /* else if (player.transform.position.x >= cameraStableRightRange)
            {
                target = player.transform.position.x + cameraOffset;
                cameraPosition.x = Mathf.Lerp(cameraPosition.x, target, Time.deltaTime * cameraFollowSpeed);
            }
            
            else if (player.transform.position.y >= cameraStableUpRange)
            {
                target = player.transform.position.y + cameraOffset;
                cameraPosition.y = Mathf.Lerp(cameraPosition.y, target, Time.deltaTime * cameraFollowSpeed);
            }
            else
            {
                float targetX = player.transform.position.x + cameraOffset;
                float targetY = player.transform.position.y + cameraOffset;
                cameraPosition.x = Mathf.Lerp(cameraPosition.x, targetX, Time.deltaTime * cameraAdjustingSpeed);
                cameraPosition.y = Mathf.Lerp(cameraPosition.y, targetY, Time.deltaTime * cameraAdjustingSpeed);
            }*/
            transform.position = cameraPosition;
        }
       
    }
}
