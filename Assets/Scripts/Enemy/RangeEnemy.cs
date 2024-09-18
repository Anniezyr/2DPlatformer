using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeEnemy : MonoBehaviour
{
    [Header("Attack Parameters")]
    [SerializeField] private float AttackCoolDown;
    [SerializeField] private int Damage;
    [SerializeField] private float range;

    [Header("Collider Parameters")]
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private float colliderDistance;

    [Header("Player Parameters")]
    [SerializeField] private LayerMask PlayerLayer;
    private float coolDownTimer = Mathf.Infinity;

    //reference
    private Animator anim;
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
            if (coolDownTimer >= AttackCoolDown)
            {
                //Attack
                coolDownTimer = 0;
                anim.SetTrigger("RangeAttack");
            }
        }

        if (enemypatrol != null)
        {
            enemypatrol.enabled = !PlayerInSignt();// when player is not in sight, enemy patrol = true;
        }

    }

    private void RangeAttack()
    {
        coolDownTimer = 0;
        //shoot the protectile
    }


    private bool PlayerInSignt()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
            0, Vector2.left, 0, PlayerLayer);

        return hit.collider != null;
    }

    private void OnDrawGizmos()//Debug for the raycast
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }

}
