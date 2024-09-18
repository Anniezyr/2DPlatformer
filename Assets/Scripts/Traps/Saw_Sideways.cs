using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Sideways : MonoBehaviour
{
    [SerializeField]private float MoveDistance;
    [SerializeField] private float speed;

    private bool moveleft;
    private float leftedge;
    private float rightedge;

    private void Awake()
    {
        leftedge = transform.position.x - MoveDistance;
        rightedge = transform.position.x + MoveDistance;
    }

    private void Update()//enemy moving
    {
        if (moveleft)
        {
            if(transform.position.x > leftedge)
            {
                transform.position = new Vector3(transform.position.x - speed * Time.deltaTime, transform.position.y, transform.position.z);
            }
            else
            {
                moveleft = false;
            }
        }
        else
        {
            if (transform.position.x < rightedge)
            {
                transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y, transform.position.z);
            }
            else
            {
                moveleft = true;
            }
        }
    }

}
