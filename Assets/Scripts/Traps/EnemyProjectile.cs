using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : EnemyDamage//make damage everytime when they touch
{
    [SerializeField] private float speed;
    [SerializeField] private float resetTime;
    private float lifetime;
    private Animator anim;
    private bool hit;
    private BoxCollider2D boxcollider;

    private void Start()
    {
        anim = GetComponent<Animator>();
        boxcollider = GetComponent<BoxCollider2D>();
    }
    public void ActivateProjectile()
    {
        hit = false;
        lifetime = 0;
        gameObject.SetActive(true);
        boxcollider.enabled = true;
    }

    private void Update()
    {
        if (hit) 
            return;

        float movementspeed = speed * Time.deltaTime;
        transform.Translate(movementspeed, 0, 0);//move to the direction

        lifetime += Time.deltaTime;
        if(lifetime > resetTime)// after send the projectile for some time(resettime), deactivate it
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        hit = true;
        base.OnTriggerEnter2D(collision);//execute logic from the parent script first
        boxcollider.enabled = false;//avoid player hit by air

        if (anim != null)
            anim.SetTrigger("Explode");//for rannged enemy, fireball explode
        else
            gameObject.SetActive(false);// for the trap
    }

    private void EnemyFireballDeactivated()
    {
        gameObject.SetActive(false);
    }
}
