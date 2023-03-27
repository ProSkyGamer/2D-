using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wood : Entity
{
    private bool isInCollision = false;
    private bool isTakingDamage = false;
    public override void GetDamage()
    {
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == Movement.Instance.gameObject)
        {
            Movement.Instance.GetDamage();
            isInCollision = true;
        }
    }

    private void Update()
    {
        if (isInCollision && !isTakingDamage)
        {
            StartCoroutine(PeriodicalDamage());
            isTakingDamage = true;
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
}