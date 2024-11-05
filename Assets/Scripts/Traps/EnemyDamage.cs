using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] protected float Damage;
    protected void OnTriggerEnter2D(Collider2D collision)//enemy hit player
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<Health>().TakeDamage(Damage);
        }
    }
}
