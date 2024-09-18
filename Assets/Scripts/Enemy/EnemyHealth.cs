using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private float StartingHealth;
    public float currentHealth { get; private set; }
    private Animator anim;
    private bool dead;
    private SpriteRenderer spriteRend;

    private void Awake()
    {
        currentHealth = StartingHealth;
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }
    public void EnemyTakeDamage(float _damage)//reduce health
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, StartingHealth);//mathf.clamp(value, min,max)

        if (currentHealth > 0)
        {
            //get hurt
            anim.SetTrigger("Hurt");
        }
        else
        {
            //dead only once
            if (!dead)
            {
                anim.SetTrigger("Die");

                //For the Knight Enemy
                if(GetComponentInParent<EnemyPatrol>() != null)
                    GetComponentInParent<EnemyPatrol>().enabled = false;

                if(GetComponent<MeleeEnemy>() != null)
                    GetComponent<MeleeEnemy>().enabled = false;

                //gameObject.SetActive(false);
                dead = true;
            }

        }
    }

}
