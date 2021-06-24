/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CharacterController))]

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public Animator animator;

    public float speed = 23f;

    float horizontalMove = 0f;

    bool crawl = false;
  

    void Start()
    {

        animator.GetBool("IsRunning");

        animator.GetBool("IsCrawling");


    }

    // Update is called once per frame
    void Update()
    {

        horizontalMove = Input.GetAxisRaw("Horizontal") * speed;


        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));


        //keep enabling crawling when left ctrl is pressed and disable when pressed again 

        if (Input.GetKeyUp(KeyCode.LeftControl))
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

        controller.Move(horizontalMove * Time.fixedDeltaTime, crawl, false);


        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.D))
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
*/
