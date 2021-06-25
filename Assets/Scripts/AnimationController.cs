using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[System.Serializable]
public sealed class AnimationController : Controller
{
    public override void Setup()
    {
        throw new System.NotImplementedException();
    }

    //private readonly int Velocity = Animator.StringToHash("Velocity");
    private readonly int _moving = Animator.StringToHash("Moving");
    
    public void MovementDirection(Rigidbody2D playerRigidbody2D, SpriteRenderer playerSpriteRenderer)
    {
        
        //playerAnimator.SetFloat(Velocity, playerRigidbody2D.velocity.x);
    }

    public void WalkingContext(InputAction.CallbackContext context, Animator playerAnimator, Rigidbody2D playerRigidBody2D, float direction)
    {
        var transform = playerRigidBody2D.transform;
        var playerScale = transform.localScale;
        
        transform.localScale = new Vector3(direction,playerScale.y,playerScale.z);

        playerAnimator.SetBool(_moving, context.performed);
    }

}

