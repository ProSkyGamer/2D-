using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingPad : MonoBehaviour
{
    [SerializeField] private float increasedJumpForce;

    private float prevJumpForce;

    private void Start()
    {
        prevJumpForce =  Movement.Instance.jumpForce;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == Movement.Instance.gameObject)
        {
            Movement.Instance.jumpForce = increasedJumpForce;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        Movement.Instance.jumpForce = prevJumpForce;
    }
}
