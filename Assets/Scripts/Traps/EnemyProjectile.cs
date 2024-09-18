using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : EnemyDamage//make damage everytime when they touch
{
    [SerializeField] private float speed;
    [SerializeField] private float resetTime;
    private float lifetime;

    public void ActivateProjectile()
    {
        lifetime = 0;
        gameObject.SetActive(true);
    }

    private void Update()
    {
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
        base.OnTriggerEnter2D(collision);//execute logic from the parent script first
        gameObject.SetActive(false);
    }
}
