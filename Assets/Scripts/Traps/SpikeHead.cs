using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeHead : EnemyDamage
{
    [Header("SpikeHead Attributes")]
    [SerializeField] private float speed;
    [SerializeField] private float range;// how far it can "see"
    [SerializeField] private float checkDelay;
    [SerializeField] private LayerMask PlayerLayer;
    private float checkTimer;
    private Vector3[] directions = new Vector3[4];//only has 4 elements
    private Vector3 destination;
    private bool attacking;

    private void OnEnable()// make sure it is idling when start the game
    {
        Stop();
    }

    private void Update()
    {
        //move to the destination only when attacking
        if(attacking)
            transform.Translate(destination * speed * Time.deltaTime);
        else
        {
            checkTimer += Time.deltaTime;

            //after delay, checking for player's position
            if (checkTimer > checkDelay)
                CheckForPlayer();
        }
    }

    private void CheckForPlayer()
    {
        CalculateDirection();

        //check if it sees player in 4 direction
        for (int i = 0; i < directions.Length; i++)
        {
            Debug.DrawRay(transform.position, directions[i], Color.red);//(start, direction, color)
            RaycastHit2D hit = Physics2D.Raycast(transform.position,directions[i],range,PlayerLayer);

            if(hit.collider != null && !attacking)//if raycast find player
            {
                attacking = true;
                destination = directions[i];
                checkTimer = 0;
            }
        }
    }

    private void CalculateDirection()//check the 4 direction and calculate
    {
        directions[0] = transform.right * range;//right direction range
        directions[1] = -transform.right * range;//left direction range
        directions[2] = transform.up * range;//up direction range
        directions[3] = -transform.up * range;//down direction range
    }

    private void Stop()
    {
        //set destination to current position
        destination = transform.position;
        attacking = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);

        //stop after hitting something
        Stop();
    }
}
