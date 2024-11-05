using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [Header("Patrol Parameters")]
    [SerializeField] private Transform LeftEdge;
    [SerializeField] private Transform RightEdge;

    [Header("Enemy Parameters")]
    [SerializeField] private Transform enemy;
    [SerializeField] private Animator anim;

    [Header("Movement Parameters")]
    [SerializeField] private float speed;
    private Vector3 intialScale;
    private bool movingLeft;
    [SerializeField] private float IdleDuration;
    private float IdleTimer;

    private void OnDisable()
    {
        //when enemypatrol is disabled (in the meleeEnemy script), the animation stop
        anim.SetBool("Moving", false);
    }

    private void Awake()
    {
        intialScale = enemy.localScale;
    }
    private void Update()
    {
        if (movingLeft)
        {
            if (enemy.position.x >= LeftEdge.position.x)
                MoveInDirection(-1);
            else//reach the left edge, need to change direction
                DirectionChange();
        }
        else
        {
            if (enemy.position.x <= RightEdge.position.x)
                MoveInDirection(1);
            else
                DirectionChange();
        }
       
    }
    private void DirectionChange()
    {
        anim.SetBool("Moving", false);

        IdleTimer += Time.deltaTime;

        if(IdleTimer > IdleDuration)
            movingLeft = !movingLeft;
    }

    private void MoveInDirection(int _direction)
    {
        anim.SetBool("Moving", true);
        IdleTimer = 0;
        //face the right direction
        enemy.localScale = new Vector3(Mathf.Abs(intialScale.x) * _direction, 
            intialScale.y, intialScale.z);
        //move to the direction
        enemy.position = new Vector3(enemy.position.x + speed * Time.deltaTime * _direction,
            enemy.position.y, enemy.position.z);
    }
}
