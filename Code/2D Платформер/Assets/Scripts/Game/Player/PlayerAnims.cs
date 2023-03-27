using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnims : MonoBehaviour
{
    private Animator anim;

    private static PlayerAnims instance;

    public static PlayerAnims Instance { get => instance; set => instance = value; }

    private void Awake()
    {
        if (Instance != null)
            Destroy(this.gameObject);
        else
            Instance = this;

        anim = this.gameObject.GetComponent<Animator>();
    }

    public PlayerStates PlayerState
    {
        get { return (PlayerStates)anim.GetInteger("state"); }
        private set { anim.SetInteger("state", (int)value); }
    }

    public AttackStates AttackState
    {
        get { return (AttackStates)anim.GetInteger("attack"); }
        private set { anim.SetInteger("attack", (int)value); }
    }

    public void ChangePlayerState(int newState)
    {
        AttackState = AttackStates.NotAttack;
        PlayerState = (PlayerStates)newState;
    }

    public void ChangeAttackState(int newState)
    {
        PlayerState = PlayerStates.Attacking;
        AttackState = (AttackStates)newState;
    }

    public enum PlayerStates
    {
        Iddle,
        Run,
        Jump,
        Fall,
        Hit,
        Death,
        Dead, 
        Attacking
    }

    public enum AttackStates
    {
        Attack1,
        Attack2,
        Attack3,
        Attack4,
        Attack5,
        NotAttack,
    }
}
