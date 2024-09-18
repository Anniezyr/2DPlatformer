using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float AttackCoolDown;
    [SerializeField] private Transform FirePoint;
    [SerializeField] private GameObject[] FireBalls;

    private Animator anim;
    private PlayerMovement playerMovement;
    private float CoolDownTimer = Mathf.Infinity;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && CoolDownTimer>AttackCoolDown && playerMovement.CanAttack())//if mouse left click and cool down is finished and playermovement script check player is able to attack
            Attack();

        CoolDownTimer += Time.deltaTime;
    }

    private void Attack()
    {
        anim.SetTrigger("Attack");
        CoolDownTimer = 0;// reset the cool down timer

        //pool Fireball( reuse fireball instead destory them)
        FireBalls[FindFireBall()].transform.position = FirePoint.position;
        FireBalls[FindFireBall()].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
    }

    private int FindFireBall()// find the fireball that is not activated and retunre the number
    {
        for (int i = 0; i < FireBalls.Length; i++)
        {
            if (!FireBalls[i].activeInHierarchy)
                return i;
        }
        return 0;
    }
}
