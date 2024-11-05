using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    [Header("Attack Parameters")]
    [SerializeField] private float AttackCoolDown;
    [SerializeField] private int Damage;
    [SerializeField] private float range;
    [SerializeField] private AudioClip attacksound;
    
    [Header("Collider Parameters")]
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private float colliderDistance;

    [Header("Player Parameters")]
    [SerializeField] private LayerMask PlayerLayer;
    private float coolDownTimer = Mathf.Infinity;

    //reference
    private Animator anim;
    private Health health;
    private EnemyPatrol enemypatrol;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        enemypatrol = GetComponentInParent<EnemyPatrol>();
    }
    private void Update()
    {
        coolDownTimer += Time.deltaTime;

        //Attack when player in sight
        if (PlayerInSignt())
        {
            if(coolDownTimer >= AttackCoolDown && health.currentHealth > 0)
            {
                //Attack
                coolDownTimer = 0;
                anim.SetTrigger("MeleeAttack");

                //sound effect
                SoundFXManager.instance.PlaySound(attacksound);
               
            }
        }

        if(enemypatrol != null)
        {
            enemypatrol.enabled = !PlayerInSignt();// when player is not in sight, enemy patrol = true;
        }

    }

    private bool PlayerInSignt()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center+transform.right* range * transform.localScale.x * colliderDistance,
            new Vector3 (boxCollider.bounds.size.x* range, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
            0,Vector2.left,0,PlayerLayer);

        if(hit.collider != null)
        {
            health = hit.transform.GetComponent<Health>();
        }
        return hit.collider != null;
    }

    private void OnDrawGizmos()//Debug for the raycast
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance, 
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }

    private void DamagePlayer()
    {
        //if Player is still in sight (may run away)
        if (PlayerInSignt())
        {
            //Damage Player
            health.TakeDamage(Damage);
        }
    }
}
