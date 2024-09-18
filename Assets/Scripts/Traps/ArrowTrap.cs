using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTrap : MonoBehaviour
{
    [SerializeField] private float AttackCoolDown;
    [SerializeField] private Transform FirePoint;
    [SerializeField] private GameObject[] FireArrows;
    private float CoolDownTimer;

    private void Attack()
    {
        CoolDownTimer = 0;
        FireArrows[FindFireArrow()].transform.position = FirePoint.position; //reset the location of arrow
        FireArrows[FindFireArrow()].GetComponent<EnemyProjectile>().ActivateProjectile();
    }

    private int FindFireArrow()
    {
        for (int i = 0; i < FireArrows.Length; i++)
        {
            if (!FireArrows[i].activeInHierarchy)
                return i;
        }
        return 0;
    }
    private void Update()
    {
        CoolDownTimer += Time.deltaTime;

        if(CoolDownTimer >= AttackCoolDown)//after cool down, direct attack
            Attack();
    }
}
