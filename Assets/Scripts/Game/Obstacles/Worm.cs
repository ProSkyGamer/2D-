using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worm : Entity
{

    private bool isInCollision = false;
    private bool isTakingDamage = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject==Movement.Instance.gameObject)
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
        yield return new WaitForSeconds(1.5f);
        if (isTakingDamage)
            Movement.Instance.GetDamage();
        isTakingDamage = false;
    }
}
