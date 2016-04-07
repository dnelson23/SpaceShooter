using UnityEngine;
using System.Collections.Generic;

namespace Assets.Scripts.Components
{
    [RequireComponent(typeof(Rigidbody))]
    class RigidBodyMovement2D : CustomComponentBase
    {
        public Vector3 moveVector { get; set; }

        // These two properties can be overwritten in your controller
        public float acceleration = 10f;
        public float maxSpeed = 20f;

        Rigidbody rBody;
        float rigidbodyDrag = 1.5f;

        void FixedUpdate()
        {
            rBody.AddForce(moveVector);
        }

        public override void Load(GameObject parent)
        {
            base.Load(parent);
            moveVector = _parent.position;
            rBody = _parent.GetComponent<Rigidbody>();
            rBody.useGravity = false;
            rBody.drag = rigidbodyDrag;
        }

        public void SetMoveVector(Vector3 vect)
        {
            vect.y = 0f;
            moveVector = vect * acceleration;
            if(moveVector.magnitude >= maxSpeed)
            {
                moveVector = Vector3.Normalize(moveVector) * maxSpeed;
            }
        }

        public void Rotate(Vector3 dir, float speed)
        {
            float step = speed * Time.deltaTime;
            Vector3 newLookRot = Vector3.RotateTowards(_parent.forward, dir, step, 0.0F);
            _parent.rotation = Quaternion.LookRotation(newLookRot);
        }
    }
}
