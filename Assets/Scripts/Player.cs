using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;


public class Player : Fighter
{


    public float moveSpeed = 5f;
    public float actualMoveSpeed;
    //public Animator death;
    public Rigidbody2D rb;
    public Animator animator;
    Vector2 movement;
    private float dashSpeed;
    private float dashLength = 0.1f, dashCooldown = 1f;
    private float dashCounter=0;
    private float dashCoolCounter = 0f;
    
    
    void Start()
    {
        actualMoveSpeed = moveSpeed;
        dashSpeed = 20;
    }
    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);

        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(dashCoolCounter<= 0 && dashCounter<= 0)
            {
                actualMoveSpeed = dashSpeed;
                dashCounter = dashLength;
            }
        }
        if(dashCounter>0)
        {
            dashCounter -= Time.deltaTime;
            if(dashCounter<=0)
            {
                actualMoveSpeed = moveSpeed;
                dashCoolCounter = dashCooldown;
            }
        }
        if(dashCoolCounter > 0)
        {
            dashCoolCounter -= Time.deltaTime;
        }
    }
     void FixedUpdate()
    {
        Vector2 move = rb.position + movement * actualMoveSpeed * Time.fixedDeltaTime;
        if(Mathf.Abs(pushDirection.x) + Mathf.Abs(pushDirection.y) > 0.3 ){
            move += (pushDirection-movement) * actualMoveSpeed * Time.fixedDeltaTime;
        }
        
        pushDirection = Vector2.Lerp(pushDirection, Vector2.zero, pushRecoverySpeed);
        rb.MovePosition(move);
    }
    protected override void Death(){
       GameObject A = GameObject.Find ("death");
       animator = A.GetComponent<Animator>();
       animator.SetTrigger("Show");
       Time.timeScale = 0;
    }
}
