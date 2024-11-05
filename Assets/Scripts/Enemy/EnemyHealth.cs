using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private float StartingHealth;
    [SerializeField] private AudioClip deadsound;
    [SerializeField] private AudioClip hurtsound;
    public float currentHealth { get; private set; }
    private Animator anim;
    private bool dead;
    private SpriteRenderer spriteRend;

    [Header("Components")]
    [SerializeField]private Behaviour[] components;

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
            SoundFXManager.instance.PlaySound(hurtsound);
        }
        else
        {
            //dead only once
            if (!dead)
            {
                anim.SetTrigger("Die");

                //Deactivated all related components
                foreach (Behaviour component in components)
                {
                    component.enabled = false;
                }

                //gameObject.SetActive(false);
                SoundFXManager.instance.PlaySound(deadsound);
                dead = true;
            }
        }
    }

    public void Deactivated()
    {
        gameObject.SetActive(false);
    }

}
