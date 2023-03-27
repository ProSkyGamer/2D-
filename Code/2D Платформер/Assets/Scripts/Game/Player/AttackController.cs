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

    private LayerMask enemy;

    [HideInInspector] public bool isAttacking = false;
    private bool isRecharged = true;

    private Transform attackPos;
    private float attackRange;

    private void Start()
    {
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

        if (maxcomboAttack > 5)
            maxcomboAttack = 5;
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
            PlayerAnims.Instance.ChangeAttackState(currAtk);

            isAttacking = true;
            isRecharged = false;
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
        isAttacking = false;
        PlayerAnims.Instance.ChangePlayerState((int)PlayerAnims.PlayerStates.Iddle);
        

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
        if (attackPos != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(attackPos.position, attackRange);
        }
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
}
