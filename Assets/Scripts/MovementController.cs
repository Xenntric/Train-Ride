using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn;

[System.Serializable]
public sealed class MovementController : Controller
{
    public float movementSpeed;
    public float acceleration = 4;
    public override void Setup()
    {
        throw new System.NotImplementedException();
    }

    // Update is called once per frame
    public void MovementUpdate(Rigidbody2D playerRigidBody, Vector2 value)
    {
        var currentVelocity = playerRigidBody.velocity;
        var accelerationLerp = Vector2.Lerp(currentVelocity, movementSpeed * value, acceleration * Time.deltaTime);
        
        playerRigidBody.velocity = new Vector2(accelerationLerp.x,playerRigidBody.velocity.y);
    }

    public float GetVelocity(Rigidbody2D playerRigidbody)
    {
        return playerRigidbody.velocity.x;
    }

    public float GetNewDirection(Vector2 movementVector, Vector3 playerScale)
    {
        var newDirection = playerScale.x;
        var movementVectorX = (int) movementVector.x;
        if (movementVectorX != 0)
        {
            newDirection = playerScale.x * movementVectorX;
        }
        return newDirection;
    }
}