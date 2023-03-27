using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingMonster : Entity
{
    [SerializeField] private float speed;

    private bool isInCollision = false;
    private bool isTakingDamage = false;


    private Vector3 dir;
    private SpriteRenderer sprite;

    private Animator anim;

    private void Start()
    {
        dir = transform.right;
    }

    private void Awake()
    {
        anim = GetComponent<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    private States State
    {
        get { return (States)anim.GetInteger("State"); }
        set { anim.SetInteger("State", (int)value); }
    }

    private void Update()
    {
        Move();
        if (isInCollision && !isTakingDamage)
        {
            StartCoroutine(PeriodicalDamage());
            isTakingDamage = true;
        }
    }

    private void Move()
    {
        float offset = 0.1f;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position + transform.up * offset + transform.right * dir.x * offset, offset);

        if (colliders.Length > 1)
        {
            dir *= -1f;
            sprite.flipX = dir.x < 0.0f;
        }
        transform.position = Vector3.MoveTowards(transform.position, transform.position + dir, Time.deltaTime * speed);
        State = States.Walk;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == Movement.Instance.gameObject)
        {
            Movement.Instance.GetDamage();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isInCollision = false;
        isTakingDamage = false;
        StopCoroutine(PeriodicalDamage());
    }

    private IEnumerator PeriodicalDamage()
    {
        float delayBetwenDamage = 1.5f;
        yield return new WaitForSeconds(delayBetwenDamage);

        if (isTakingDamage)
            Movement.Instance.GetDamage();
        isTakingDamage = false;
    }


    public enum States
    {
        Iddle,
        Walk,
    }
}
