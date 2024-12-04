using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    Vector2 moveInput;
    Rigidbody2D myrigidbody;
    Animator myanimator;
    CapsuleCollider2D myBodyCollider;
    BoxCollider2D myFeetCollider;
    SpriteRenderer myspriterenderer;
    [SerializeField] TrailRenderer tr;

    [SerializeField] float runSpeed = 1f;
    [SerializeField] float jumpForce = 1f;
    [SerializeField] Vector2 deathkick = new Vector2(10f, 10f);
    [SerializeField] GameObject bullet_effect;
    [SerializeField] GameObject bullet;
    [SerializeField] Transform gun;
    bool isAlive = true;
    bool canDash = true;
    bool isDashing = false;
    [SerializeField] float dashingPower = 25f;
    [SerializeField] float dashingTime = 0.2f;
    [SerializeField] float dashingCoolDown = 1f;

    AudioManager audi;


    void Start()
    {
        myrigidbody = GetComponent<Rigidbody2D>();
        myanimator = GetComponent<Animator>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        myFeetCollider = GetComponent<BoxCollider2D>();
        myspriterenderer = GetComponent<SpriteRenderer>();
        tr = GetComponent<TrailRenderer>();
    }

    void Update()
    {
        if (!isAlive) return;
        if (isDashing) return;
        Run();
        flipSprite();
        jumpAnimation();
        Die();

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
        }
    }

    void OnMove(InputValue value)
    {
        if (!isAlive) return;
        moveInput = value.Get<Vector2>();
    }

    void OnFire(InputValue value)
    {
        if (!isAlive) return;
        Destroy(Instantiate(bullet_effect, gun.position, transform.rotation), 0.8f);
        Instantiate(bullet, gun.position, transform.rotation);

    }

    void Run()
    {
        Vector2 playerVeloctiy = new Vector2(moveInput.x * runSpeed, myrigidbody.velocity.y);
        myrigidbody.velocity = playerVeloctiy;
        bool playerHasHorizontalSpeed = Mathf.Abs(myrigidbody.velocity.x) > Mathf.Epsilon;
        myanimator.SetBool("isRunning", playerHasHorizontalSpeed);
    }

    void jumpAnimation()
    {
        if (myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            myanimator.SetBool("isJumping", false);
        }
        else
        {
            myanimator.SetBool("isJumping", true);
        }
    }

    void OnJump(InputValue value)
    {
        if (!isAlive) return;
        if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            return;
        }

        if (value.isPressed)
        {
            myrigidbody.velocity += new Vector2(0, jumpForce);
        }

    }
    void flipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(myrigidbody.velocity.x) > Mathf.Epsilon;

        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(myrigidbody.velocity.x), 1f);
        }
    }

    public void Die()
    {
        if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemies", "Hazards")))
        {
            isAlive = false;
            myanimator.SetTrigger("death");
            myspriterenderer.color = Color.red;
            myrigidbody.velocity = deathkick;
            FindObjectOfType<GameSession>().ProcessPlayerDeath();
        }
    }

    IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = myrigidbody.gravityScale;
        myrigidbody.gravityScale = 0f;
        myrigidbody.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        tr.emitting = true;
        myanimator.SetBool("isDashing", true);
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        myrigidbody.gravityScale = originalGravity;
        isDashing = false;
        myanimator.SetBool("isDashing", false);
        yield return new WaitForSeconds(dashingCoolDown);
        canDash = true;
    }

}
