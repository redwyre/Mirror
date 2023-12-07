using UnityEngine;
using UnityEngine.AI;

namespace Mirror.Examples.RigidbodyCollisions
{
    public class PlayerCube : NetworkBehaviour
    {
        [Header("Components")]
        public new Rigidbody rigidbody;

        [Header("Movement")]
        public float movementSpeed = 10.0f;
        public float rotationSpeed = 100.0f;

        Vector3 forward;
        float yaw;

        void Update()
        {
            // take input from focused window only
            if (!Application.isFocused)
            {
                forward = Vector3.zero;
                yaw = 0;
                return;
            }

            // movement for local player
            if (isLocalPlayer)
            {
                float vertical = Input.GetAxis("Vertical");
                forward = transform.TransformDirection(Vector3.forward) * vertical * movementSpeed;

                float horizontal = Input.GetAxis("Horizontal");
                yaw = Mathf.Deg2Rad * (horizontal * rotationSpeed);
            }
        }

        private void FixedUpdate()
        {
            // movement for local player
            if (isLocalPlayer)
            {
                // move
                rigidbody.velocity += forward * Time.fixedDeltaTime;
                rigidbody.angularVelocity += new Vector3(0.0f, yaw, 0.0f);
            }
        }

        //[ServerCallback]
        //void OnTriggerEnter(Collider other)
        //{
        //}
    }
}
