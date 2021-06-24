using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Controllers
{
    [System.Serializable]
    public sealed class MovementController : Controller
    {
        public float _movementSpeed;
        public float _acceleration = 4;
        public override void setup()
        {
            throw new System.NotImplementedException();
        }

        // Update is called once per frame
        public void MovementUpdate(Rigidbody2D playerRigidBody, Vector2 value)
        {
            var currentVelocity = playerRigidBody.velocity;
            var lerpy = Vector2.Lerp(currentVelocity, _movementSpeed * value, _acceleration * Time.deltaTime);
            playerRigidBody.velocity = new Vector2(lerpy.x,playerRigidBody.velocity.y);
        }
    }
}