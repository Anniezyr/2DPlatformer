using UnityEngine;
using System.Collections;

public class FireTrap : MonoBehaviour
{
    [SerializeField] private float Damage;

    [Header("FireTrap Timers")]
    [SerializeField] private float activationDelay;
    [SerializeField] private float activateTime;
    private Animator anim;
    private SpriteRenderer spriteRend;

    private bool Triggered;//when player trigger the trap
    private bool Activated;//the trap can hurt player

    private void Awake()
    {
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if (!Triggered)
            {
                //trigger the trap
                StartCoroutine(ActiveFireTrap());
            }
            if (Activated)
            {
                collision.GetComponent<Health>().TakeDamage(Damage);
            }
        }
    }

    private IEnumerator ActiveFireTrap()
    {
        //turn on the trap, turn red to notify player about that
        Triggered = true;
        spriteRend.color = new Color(1,0,0,0.5f);

        //wait for delay, activate trap,turn on animation,return color back to normal
        yield return new WaitForSeconds(activationDelay);
        Activated = true;
        spriteRend.color = Color.white;
        anim.SetBool("Activated",true);

        //after activate time, deactivate the trap and reset parameters
        yield return new WaitForSeconds(activateTime);
        Activated = false;
        Triggered = false;
        anim.SetBool("Activated", false);

    }
}
