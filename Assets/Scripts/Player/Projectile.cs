using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed;
    private Animator anim;
    private BoxCollider2D boxcollider;
    private bool Hit;
    private float direction;
    private float lifetime;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        boxcollider = GetComponent<BoxCollider2D>();

    }

    private void Update()
    {
        if (Hit) return;
        float movementSpeed = speed * Time.deltaTime * direction;
        transform.Translate(movementSpeed, 0, 0);//move the fireball

        lifetime += Time.deltaTime;
        if (lifetime > 5) gameObject.SetActive(false);//reset the fireball if hit nothing

    }

    public void SetDirection(float _direction)// when fire the fireball. reset everthing
    {
        lifetime = 0;
        direction = _direction;
        gameObject.SetActive(true);
        Hit = false;
        boxcollider.enabled = true;

        float localScale = transform.localScale.x;
        if (Mathf.Sign(localScale) != _direction)
            localScale = -localScale;

        transform.localScale = new Vector3(localScale, transform.localScale.y, transform.localScale.z);
    }
    private void OnTriggerEnter2D(Collider2D collision)// when fireball Hits item
    {
        Hit = true;
        boxcollider.enabled = false; //disable the collision
        anim.SetTrigger("Explode");//run animation

        if(collision.tag == "Enemy")
        {
            collision.GetComponent<Health>().TakeDamage(1);
        }
    }

    private void Deactivate()//after hit item
    {
        gameObject.SetActive(false);
    }

}
