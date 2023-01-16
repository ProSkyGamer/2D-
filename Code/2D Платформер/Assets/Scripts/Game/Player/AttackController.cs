using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    [SerializeField] private int maxcomboAttack;
    private int comboAtk = 0;
    private int currAtk = 1;
    private bool isComboFull = true;

    [SerializeField] private GameObject allTranformsRight;
    [SerializeField] private GameObject allTranformsLeft;

    [SerializeField] private List<float> allAttackRanges;

    [SerializeField] private List<Transform> transformPostionsRight;
    [SerializeField] private List<Transform> transformPostionsLeft;

    private Animator anim;
    private LayerMask enemy;

    [HideInInspector] public bool isAttacking = false;
    private bool isRecharged = true;

    private Transform attackPos;
    private float attackRange;

    private States Attack
    {
        get { return (States)anim.GetInteger("attack"); }
        set { anim.SetInteger("attack", (int)value); }
    }

    private States State
    {
        get { return (States)anim.GetInteger("state"); }
        set { anim.SetInteger("state", (int)value); }
    }

    private void Start()
    {
        anim = GetComponent<Animator>();
        for (int i = 1; i < allTranformsRight.GetComponentsInChildren<Transform>().Length; i++)
        {
            transformPostionsRight.Add(allTranformsRight.GetComponentsInChildren<Transform>()[i]);
        }
        for (int i = 1; i < allTranformsLeft.GetComponentsInChildren<Transform>().Length; i++)
        {
            transformPostionsLeft.Add(allTranformsLeft.GetComponentsInChildren<Transform>()[i]);
        }
        attackPos = transformPostionsRight[0];
        enemy = LayerMask.GetMask("Monsters");
    }

    public void ChangeDirectionAtkToRight()
    {
        attackPos = transformPostionsRight[currAtk - 1];
    }

    public void ChangeDirectionAtkToLeft()
    {
        attackPos = transformPostionsLeft[currAtk];
    }

    public void ChangeDirectionAtk(float x)
    {
        attackPos = x < 0.0f ? transformPostionsLeft[currAtk - 1] : transformPostionsRight[currAtk - 1];
    }

    private void Update()
    {
        if (gameObject.GetComponent<Movement>().isGrounded && isRecharged && !isAttacking &&
            !gameObject.GetComponent<Movement>().menuOpened && !gameObject.GetComponent<Movement>().isDead)
            if (Input.GetButtonDown("Fire1"))
            {
                StartAttackAnim();
            }
    }

    public void StartAttackAnim()
    {
        if (currAtk <= maxcomboAttack)
        {
            if (currAtk == 1)
            {
                Attack = States.Attack1;
            }
            else if (currAtk == 2)
            {
                Attack = States.Attack2;
            }
            else if (currAtk == 3)
            {
                Attack = States.Attack3;
            }
            else if (currAtk == 4)
            {
                Attack = States.Attack4;
            }
            else if (currAtk == 5)
            {
                Attack = States.Attack5;
            }
            isAttacking = true;
            isRecharged = false;
            anim.SetInteger("state", 100);
            print(attackPos.localPosition + " " + currAtk);
        }
    }

    public void AfterAnimAttack()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(attackPos.position, attackRange, enemy);

        for (int i = 0; i < colliders.Length; i++)
        {
            colliders[i].GetComponent<Entity>().GetDamage();
        }

        StartCoroutine(ComboCoolDown());

        if (currAtk != 1)
        {
            comboAtk += 1;
        }
        else if (currAtk == maxcomboAttack)
        {
            isComboFull = true;
        }
        if (currAtk != maxcomboAttack)
        {
            currAtk += 1;
            ChangeAttack();
            StartCoroutine(AttackCoolDown());
        }
        anim.SetInteger("state", 0);
        anim.SetInteger("attack", 100);
        isAttacking = false;

    }

    private void ChangeAttack()
    {
        if (transformPostionsRight[currAtk - 2] == attackPos)
            attackPos = transformPostionsRight[currAtk - 1];
        else
            attackPos = transformPostionsLeft[currAtk - 1];
        attackRange = allAttackRanges[currAtk - 1];
    }

    private void ResetAttack(int prevAtkCombo)
    {
        currAtk = 1;
        if (transformPostionsRight[prevAtkCombo - 1] == attackPos)
            attackPos = transformPostionsRight[currAtk - 1];
        else
            attackPos = transformPostionsLeft[currAtk - 1];
        attackRange = allAttackRanges[currAtk - 1];
        isComboFull = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }

    private IEnumerator AttackCoolDown()
    {
        yield return new WaitForSeconds(0.3f);
        if (!isComboFull)
        {
            isRecharged = true;
        }
    }

    private IEnumerator ComboCoolDown()
    {
        yield return new WaitForSeconds(1f);
        if (comboAtk != 0)
        {
            comboAtk -= 1;
        }
        else if (comboAtk == 0)
        {
            ResetAttack(currAtk);
            isRecharged = true;
        }
    }

    public enum States
    {
        Attack1,
        Attack2,
        Attack3,
        Attack4,
        Attack5,
    }
}
