using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerDriver : MonoBehaviour
{
    private Vector2 _movementVector;

    public MovementController movementController;
    public AnimationController animationController;
    public Rigidbody2D playerRigidBody;
    //public SpriteRenderer playerSpriteRenderer;
    public Animator playerAnimator;

    private Vector3 _playerScale;
    // Start is called before the first frame update
    void OnEnable()
    {
        playerRigidBody = transform.GetChild(0).GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();

        _playerScale = playerRigidBody.transform.localScale;

    }

    // Update is called once per frame
    void Update()
    {
        movementController.MovementUpdate(playerRigidBody, _movementVector);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        animationController.WalkingContext(context, playerAnimator, playerRigidBody, movementController.GetNewDirection(_movementVector, _playerScale));
        
        _movementVector =  context.ReadValue<Vector2>();

    }
}
