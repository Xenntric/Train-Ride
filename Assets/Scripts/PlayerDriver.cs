using System.Collections;
using System.Collections.Generic;
using Controllers;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerDriver : MonoBehaviour
{
    private Vector2 _movementVector;

    public MovementController MovementController;
    public Rigidbody2D playerRigidBody;
    // Start is called before the first frame update
    void OnEnable()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        MovementController.MovementUpdate(playerRigidBody, _movementVector);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        _movementVector = context.ReadValue<Vector2>();
    }
}
