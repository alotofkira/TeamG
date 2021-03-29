//------------------------------------------------------------------------------
//
// File Name:	PlayerAnimationManager.cs
// Author(s):	Jeremy Kings (j.kings) - Unity Project
//              Nathan Mueller - original Zero Engine project
//              Alex Dzius - Tech Lead on Team G in Endless Runner Project
// Project:		Endless Runner
// Course:		WANIC VGP
//
// Copyright © 2021 DigiPen (USA) Corporation.
//
//------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerAnimationStates
{
    Run,
    Jump,
    Slide,
    Hurt
};

public class PlayerAnimationManager : MonoBehaviour
{
    public float InvulnTime = 0.25f;
    public PlayerAnimationStates CurrentState = PlayerAnimationStates.Run;
    public PlayerAnimationStates PreviousState = PlayerAnimationStates.Run;

    private float CurrInvulnTime = 0;
    private Animator animator;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        SwitchTo(CurrentState);
        animator = GetComponent<Animator>();
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Override the animation that should be happening if we are hurt
        CurrInvulnTime -= Time.deltaTime;
        if (CurrInvulnTime > 0 && CurrentState != PlayerAnimationStates.Hurt)
        {
            SwitchTo(PlayerAnimationStates.Hurt);
        }
        // Switch back to the correct animation if the player is done with the hurt animation.
        if (CurrInvulnTime <= 0 && CurrentState == PlayerAnimationStates.Hurt)
        {
            SwitchTo(PreviousState);
        }

        PlayCurrentAnimation();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.otherCollider.gameObject.CompareTag("Floor"))
        {
            SwitchTo(PlayerAnimationStates.Run);
        }
    }

    public void SwitchTo(PlayerAnimationStates state)
    {
        // Ignore any animation changes if the player was hurt, we are playing that animation.
        if (CurrInvulnTime > 0)
        {
            return;
        }
      
        if (CurrentState != PlayerAnimationStates.Hurt && state == PlayerAnimationStates.Hurt)
        {
            CurrInvulnTime = InvulnTime;
        }
        PreviousState = CurrentState;
        CurrentState = state;
    }

    public void PlayCurrentAnimation()
    {
        //fix for transition, allows for proper transition between animations
        if (rb.velocity.y < 0)
        {
            animator.Play("Slide");
        }
        else if (rb.velocity.y > 0)
        {
            animator.Play("Jump");
        }
        else
        {
            animator.Play("Run");
        }
        // Don't bother if we haven't
        // switched states recently
        if (CurrentState == PreviousState)
            return;


        // Play state based on value of CurrentState
        switch (CurrentState)
        {
            case PlayerAnimationStates.Run:
                animator.Play("Run");
                break;
            case PlayerAnimationStates.Jump:
                if(rb.velocity.y < 0)
                {
                    animator.Play("Slide");
                }
                else
                {
                    animator.Play("Jump");
                }
                break;
            case PlayerAnimationStates.Slide:
                animator.Play("Slide");
                break;
            case PlayerAnimationStates.Hurt:
                animator.Play("Hurt");
                break;
        }
    }
}
