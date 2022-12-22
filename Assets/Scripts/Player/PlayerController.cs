using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float speed;
    [SerializeField] private float jumpingForce;
    [SerializeField] private float blinkLength;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private int blinkCooldown = 5;

    private float horizontal;
    private bool isFacingRight = true;
    private bool canWallJump;
    private bool isOnWall;
    private Vector2 wallNormal;
    Animator anim;

    PlayerControls playerControls;


    private void Awake()
    {
        playerControls = new PlayerControls();

    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        
        anim.SetBool("IsGrounded", IsGrounded());
        anim.SetFloat("VelocityX", Mathf.Abs(rb.velocity.x));

        Flip();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        horizontal = context.ReadValue<Vector2>().x;
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        // Do nothing if game is paused
        if (GameManager.SharedInstance.gameIsPaused) return;

        ResetGravity();
        if (context.performed && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingForce);
        } else if (context.performed && isOnWall)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingForce * 1.5f);
            isOnWall = false;
        }

    }

    private void Flip()
    {
        // Do nothing if game is paused
        if (GameManager.SharedInstance.gameIsPaused) return;

        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    public void OnBlink(InputAction.CallbackContext context)
    {
        // Do nothing if game is paused
        if (GameManager.SharedInstance.gameIsPaused) return;

        // Do nothing if blink cooldown is not finished
        if (!GameManager.SharedInstance.blinkStatus) return;
        
        if (context.ReadValue<Vector2>().y > 0.5f)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y + blinkLength);
        } else if(isFacingRight)
        {
            transform.position = new Vector2(transform.position.x + blinkLength, transform.position.y);
        } else if (!isFacingRight)
        {
            transform.position = new Vector2(transform.position.x - blinkLength, transform.position.y);
        }

        UpdateCanBlink(false);

        StartCoroutine(BlinkCoolDown());

    }
    

    IEnumerator BlinkCoolDown()
    {
        yield return new WaitForSeconds(blinkCooldown);

        UpdateCanBlink(true);
    }

    private void UpdateCanBlink(bool canBlinkStatus)
    {
        GameManager.SharedInstance.blinkStatus = canBlinkStatus;
        HUDManager.SharedInstance.UpdateBlinkStatus();
    }

    public void Bounce()
    {
        rb.AddForce(new Vector2(0, 500f));
    }
    public void MakeInvulnerable()
    {
        StartCoroutine(Blink());
        StartCoroutine(Invulnerable());
    }

    private IEnumerator Blink()
    {
        SpriteRenderer playerSprite = GetComponent<SpriteRenderer>();

        Color defaultColor = playerSprite.color;

        playerSprite.color = new Color(1, 1, 1, 1);

        yield return new WaitForSeconds(3f);

        playerSprite.color = defaultColor;
    }

    private IEnumerator Invulnerable()
    {
        GameManager.SharedInstance.isInvulnerable = true;
        yield return new WaitForSeconds(3);
        GameManager.SharedInstance.isInvulnerable = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        bool enemyIsDown = transform.position.y - collision.gameObject.transform.position.y > collision.gameObject.transform.lossyScale.y;
        if (collision.gameObject.tag == "Enemy" && enemyIsDown)
        {
            Bounce();
        }

        if(collision.gameObject.tag == "Wall")
        {
            isOnWall = true;
            SetWallGravity();
            rb.velocity = Vector2.zero;
        }
        else
        {
            isOnWall = false;
        }
        
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isOnWall = false;
        ResetGravity();
    }

    private void ResetGravity()
    {
        rb.gravityScale = 4f;
    }

    private void SetWallGravity()
    {
        rb.gravityScale = 0.2f;
    }

}
