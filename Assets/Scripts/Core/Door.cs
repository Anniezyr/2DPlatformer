using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Transform previousroom;
    [SerializeField] private Transform nextroom;
    [SerializeField] private CameraMovement cam;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if (collision.transform.position.x < transform.position.x)//player come from left
            {
                cam.MoveToNewRoom(nextroom);
                nextroom.GetComponent<Room>().ActiveRoom(true);
                previousroom.GetComponent<Room>().ActiveRoom(false);
            }
            else//player come from right
            {
                cam.MoveToNewRoom(previousroom);
                previousroom.GetComponent<Room>().ActiveRoom(true);
                nextroom.GetComponent<Room>().ActiveRoom(false);
            }
        }
    }

}
