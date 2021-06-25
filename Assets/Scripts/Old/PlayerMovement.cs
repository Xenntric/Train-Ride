
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public Animator animator;

    public float speed = 23f;

    Vector2 horizontalMove;

    bool crawl = false;
  

    void Start()
    {

        animator.GetBool("IsRunning");

        animator.GetBool("IsCrawling");


    }

    public void OnMove(InputAction.CallbackContext context)
    {
        horizontalMove = context.ReadValue<Vector2>();
    }
    // Update is called once per frame
    void Update()
    {
    //horizontalMove.x = Input.GetAxisRaw("Horizontal") * speed;
    
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove.x));


        //keep enabling crawling when left ctrl is pressed and disable when pressed again 

        if (Keyboard.current[Key.LeftCtrl].wasPressedThisFrame)
        {
            if (!crawl)
            {
                speed = 23 * 0.65f;
                crawl = true;
                animator.SetBool("IsCrawling", true);

            }
           

            else
            {
                speed = 23f;
                crawl = false;
                animator.SetBool("IsCrawling", false);

            }

        }
        

    }

    public void OnCrawl(bool IsCrawling)
    {
        animator.SetBool("IsCrawling", IsCrawling);
    }


    void FixedUpdate()
    {

        controller.Move(horizontalMove.x * Time.fixedDeltaTime, crawl, false);


        if ( Keyboard.current[Key.LeftShift].isPressed && Keyboard.current[Key.A].isPressed || Keyboard.current[Key.LeftShift].isPressed && Keyboard.current[Key.D].isPressed)
        {

            speed = 23 * 2f;
            animator.SetBool("IsRunning", true);

        }
        else
        {
            speed = 23f;
            animator.SetBool("IsRunning", false);
        }

       
       
    }
}