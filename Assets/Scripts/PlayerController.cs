using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using System;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed, dashSpeed, jumpForce, gravity;
    public float dashDistance, dashCoolDown, dashDistanceInFrame;
    public PhysicsMaterial2D normal, inAir;
    private Collider2D coll;
    public Image cdImage;
    private float dashDistanceLave, dashCoolDownLave, faceDirection, moveInputDirection;
    private bool dashReady = false;
    private bool isRunning = true;
    private PhysicsCheck physicsCheck;
    private Rigidbody2D rb;
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        physicsCheck = GetComponent<PhysicsCheck>();
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        InputManager.Instance.RegistPlayer(this);
        InputManager.Instance.inputControl.GameInput.Jump.started += Jump;
        InputManager.Instance.inputControl.GameInput.Dash.started += DashInit;
    }

    // Update is called once per frame
    void Update()
    {
        GetMoveDirection();
        SetJumpAnimation();
        SetCdAnimation();
        ChangeMaterial();
    }

    void FixedUpdate()
    {
        Move();
        Dash();
    }

    private void Move()
    {
        Debug.Log("Left:" + physicsCheck.isWallLeft);
        Debug.Log("Right:" + physicsCheck.isWallRight);
        if (isRunning && moveInputDirection != 0)
        {
            rb.velocity = new Vector2(moveInputDirection * speed, rb.velocity.y);
            FaceDirection();
        }
        else rb.velocity = new Vector2(0f, rb.velocity.y);
        anim.SetFloat("RunSpeed", Mathf.Abs(rb.velocity.x));
    }

    public void Jump(InputAction.CallbackContext callback)
    {
        if (!physicsCheck.isGround) return;
        rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
    }
    private void DashInit(InputAction.CallbackContext callback)
    {
        if (dashCoolDownLave <= 0)
        {
            isRunning = false;
            dashDistanceLave = dashDistance;
            dashCoolDownLave = dashCoolDown;
            cdImage.fillAmount = 1;
            dashReady = true;
            StartCoroutine(DashCoolDownCalculate());
        }
    }
    private void Dash()
    {
        if (dashReady)
        {

            if (dashDistanceLave > 0)
            {
                anim.SetBool("isDashing", dashReady);
                rb.velocity = new Vector2(transform.localScale.x * dashSpeed, 0);
                dashDistanceLave -= dashDistanceInFrame;
                //ShadowPool.Instance.GetFromPool();
                ShadowUnityPool.Instance.pool.Get();
            }
            if (dashDistanceLave <= 0 || physicsCheck.isWallRight || physicsCheck.isWallLeft)
            {
                rb.gravityScale = gravity;
                rb.velocity = new Vector2(rb.velocity.x, 8f);
                isRunning = true;
                dashReady = false;
                anim.SetBool("isDashing", dashReady);
            }
        }
    }
    private void SetJumpAnimation()
    {
        anim.SetFloat("JumpSpeed", rb.velocity.y);
        anim.SetBool("isGround", physicsCheck.isGround);
    }
    private void FaceDirection()
    {
        faceDirection = rb.velocity.x;
        if (faceDirection < 0)transform.localScale = new Vector3(-1f, 1f, 1f);
        if (faceDirection > 0)transform.localScale = new Vector3(1f, 1f, 1f);
    }
    private void GetMoveDirection()
    {
        moveInputDirection = InputManager.Instance.moveInput.x;
        if (moveInputDirection != 0) moveInputDirection /= Mathf.Abs(moveInputDirection);
    }
    private void SetCdAnimation()
    {
        cdImage.fillAmount -= 1f / dashCoolDown * Time.deltaTime;
    }
    private void ChangeMaterial()
    {
        coll.sharedMaterial = physicsCheck.isGround ? normal : inAir;
    }

    IEnumerator DashCoolDownCalculate()
    {
        while (dashCoolDownLave > 0)
        {
            dashCoolDownLave -= Time.deltaTime;
            yield return 0;
        }
    }
}
