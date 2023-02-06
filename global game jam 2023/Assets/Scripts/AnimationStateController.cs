using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStateController : MonoBehaviour
{
    Animator animator;
    PlayerController control;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        control = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
       

        //crouch
        if (control.state == PlayerController.MovementState.crouching)
        {
            animator.SetBool("isCrawling", true);
            //animator.SetBool("isWalking", false);
        }
        //walk
        else if (control.state == PlayerController.MovementState.walking || control.state == PlayerController.MovementState.sprinting)
        {
            animator.SetBool("isWalking", true);
             //animator.SetBool("isCrawling", false);
        }
       
        else
        {
            //idle
            animator.SetBool("isWalking", false);
            animator.SetBool("isCrawling", false);
        }

        //jump
    }
}
