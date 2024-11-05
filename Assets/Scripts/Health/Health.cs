using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Health : MonoBehaviour
{
    [Header ("Health")]
    [SerializeField] private float StartingHealth;
    [SerializeField] private AudioClip deadsound;
    [SerializeField] private AudioClip hurtsound;
    public float currentHealth { get; private set; }
    private Animator anim;
    private bool dead;
    private BoxCollider2D collision;

    [Header("iFrames")]
    [SerializeField] private float iFramesDuration;// for character invulnerable
    private SpriteRenderer spriteRend;

    [Header("Components")]
    [SerializeField] private Behaviour[] components;

    private void Awake()
    {
        currentHealth = StartingHealth;
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
        collision = GetComponent<BoxCollider2D>();
    }

    public void TakeDamage(float _damage)//reduce health
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, StartingHealth);//mathf.clamp(value, min,max)
        
        if(collision.tag == "Player")
        {
            if (currentHealth > 0)
            {
                //player hurt
                anim.SetTrigger("Hurt");
                SoundFXManager.instance.PlaySound(hurtsound);

                StartCoroutine(Invulnerability());
            }
            else
            {
                //player dead only once
                if (!dead)
                {
                    anim.SetTrigger("Die");

                    if (collision.tag == "Player")
                    {

                    }
                    SoundFXManager.instance.PlaySound(deadsound);

                    GetComponent<PlayerMovement>().enabled = false;//disable the control
                    dead = true;
                }

            }
        }else if(collision.tag == "Enemy")
        {
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
        
    }

    public void TakeHealth(float _value)//add Health
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, StartingHealth);//mathf.clamp(value, min,max)
    }

    private IEnumerator Invulnerability()// flash when get hurt
    {
        Physics2D.IgnoreLayerCollision(8, 9,true);//ignore or detect layer collision between 2 layers


        //after invulnerable duration
        yield return new WaitForSeconds(iFramesDuration);//yield to pause 
        Physics2D.IgnoreLayerCollision(8, 9,false);

    }

    public void Deactivated()
    {
        gameObject.SetActive(false);
    }
}
