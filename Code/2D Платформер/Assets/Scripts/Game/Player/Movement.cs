using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Movement : Entity
{
    [SerializeField] public float speed;
    [SerializeField] public float jumpForce;

    [SerializeField] public List<Image> hearts;

    [SerializeField] private Sprite aliveHeart;
    [SerializeField] private Sprite deadHeart;

    [SerializeField] private Joystick _joystick;
    
    [SerializeField] private Canvas dead_Screen;

    [HideInInspector] public bool isDead;
    [HideInInspector] private bool isDead2 = false;

    [HideInInspector] public bool menuOpened = false;
    [HideInInspector] public bool isGrounded = false;

    [HideInInspector] public int health;
    
    private LayerMask terrain;
    private Rigidbody2D rb;
    private SpriteRenderer personSprite;

    private Vector3 dir;
    public static Movement Instance { get; set; }

    private void Awake()
    {
        if (Instance != null)
            Destroy(this.gameObject);
        else
            Instance = this;
    }

    private void Start()
    {
        lives = hearts.Count;
        health = lives;
        Instance = this;
        rb = GetComponent<Rigidbody2D>();
        personSprite = GetComponentInChildren<SpriteRenderer>();
        terrain = LayerMask.GetMask("Terrain");
    }

    public void ChangeLives()
    {
        lives = hearts.Count;
    }

    public void MenuOpenClose(bool state)
    {
        menuOpened = state;
    }

    private void Update()
    {
        if (!isDead)
        {
            if (!menuOpened)
            {
                //Iddle Anim
                if (isGrounded && !gameObject.GetComponent<AttackController>().isAttacking)
                    PlayerAnims.Instance.ChangePlayerState((int)PlayerAnims.PlayerStates.Iddle);

                //Run
                if (PlayerPrefs.GetInt("joystickEnabled") == 1)
                {
                    if (!gameObject.GetComponent<AttackController>().isAttacking && _joystick.Horizontal != 0)
                        Run(true);
                }
                else if (PlayerPrefs.GetInt("joystickEnabled") == 0)
                {
                    if (!gameObject.GetComponent<AttackController>().isAttacking && Input.GetButton("Horizontal"))
                        Run(false);
                }

                //Jump
                if (PlayerPrefs.GetInt("joystickEnabled") == 1)
                {
                    if (!gameObject.GetComponent<AttackController>().isAttacking && isGrounded && _joystick.Vertical >= 0.5f)
                        Jump();
                }
                else if (PlayerPrefs.GetInt("joystickEnabled") == 0)
                {
                    if (!gameObject.GetComponent<AttackController>().isAttacking && isGrounded && Input.GetButtonDown("Jump"))
                        Jump();
                }

                //Health
                if (health > lives)
                {
                    health = lives;
                }
                for (int i = 0; i < hearts.Count; i++)
                {
                    if (i < health)
                        hearts[i].sprite = aliveHeart;
                    else
                        hearts[i].sprite = deadHeart;

                    if (i < lives)
                        hearts[i].enabled = true;
                    else
                        hearts[i].enabled = false;
                }
            }
        }

        //Dead Anim
        else if (PlayerAnims.Instance.PlayerState != PlayerAnims.PlayerStates.Dead &&
            PlayerAnims.Instance.PlayerState != PlayerAnims.PlayerStates.Death && !isDead2)
        {
            PlayerAnims.Instance.ChangePlayerState((int)PlayerAnims.PlayerStates.Death);
            print("true");
        }
    }

    private void FixedUpdate()
    {
        if (!isDead)
        {
            CheckGround();
            if(transform.position.y < -30)
            {
                PlayerAnims.Instance.ChangePlayerState((int)PlayerAnims.PlayerStates.Death);
                Camera.main.GetComponent<CameraController>().StopCameraFollowing();
            }
        }
    }

    private void Run(bool isJoystick)
    {
        if (isGrounded)
            PlayerAnims.Instance.ChangePlayerState((int)PlayerAnims.PlayerStates.Run);

        if (isJoystick)
            dir = transform.right * _joystick.Horizontal;
        else
            dir = transform.right * Input.GetAxis("Horizontal");

        transform.position = Vector3.MoveTowards(transform.position, transform.position + dir, speed * Time.deltaTime);

        gameObject.GetComponent<AttackController>().ChangeDirectionAtk(dir.x);

        personSprite.flipX = dir.x < 0.0f;
    }

    public void Jump()
    {
        rb.velocity = Vector2.up * jumpForce * 2.25f;
    }

    private void CheckGround()
    {
        Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position, 0.2f, terrain);
        isGrounded = collider.Length > 0;

        if (!isGrounded)
        {
            if (rb.velocity.y > 0)
            {
                PlayerAnims.Instance.ChangePlayerState((int)PlayerAnims.PlayerStates.Jump);
            }
            else
            {
                PlayerAnims.Instance.ChangePlayerState((int)PlayerAnims.PlayerStates.Fall);
            }
        }
    }

    public override void GetDamage()
    {
        health -= 1;
        if (health < 1 && PlayerAnims.Instance.PlayerState != PlayerAnims.PlayerStates.Dead &&
            PlayerAnims.Instance.PlayerState != PlayerAnims.PlayerStates.Death)
        {
            isDead = true;
            foreach (Image heart in hearts)
            {
                heart.sprite = deadHeart;
            }
            PlayerAnims.Instance.ChangePlayerState((int)PlayerAnims.PlayerStates.Death);
        }
    }

    private void OnDeath()
    {
        PlayerAnims.Instance.ChangePlayerState((int)PlayerAnims.PlayerStates.Dead);
        dead_Screen.gameObject.SetActive(true);
        isDead2 = true;
    }
}
