using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pikmin
{
    /// <summary>
    /// 移动控制
    /// </summary>
    [RequireComponent(typeof(CharacterController))]
    public class MovementInput : MonoBehaviour
    {
        public float Velocity;
        [Space]

        public float InputX;
        public float InputZ;
        public Vector3 desiredMoveDirection;
        public bool blockRotationPlayer;
        public float desiredRotationSpeed = 0.1f;
        public float Speed;
        public float allowPlayerRotation = 0.1f;
        public Camera cam;
        public CharacterController controller;
        public bool isGrounded;

        public float verticalVel;
        private Vector3 moveVector;
        private void Start()
        {
            cam = Camera.main;
            controller = this.GetComponent<CharacterController>();
        }

        private void Update()
        {
            InputMagnitude();

            isGrounded = controller.isGrounded;
            if (isGrounded)
            {
                verticalVel -= 0;
            }
            else
            {
                verticalVel -= 1;
            }
            moveVector = new Vector3(0, verticalVel * .2f * Time.deltaTime, 0);
            controller.Move(moveVector);
        }

        private void PlayerMoveAndRotation()
        {
            InputX = Input.GetAxis("Horizontal");
            InputZ = Input.GetAxis("Vertical");

            var camera = Camera.main;
            var forward = cam.transform.forward;
            var right = cam.transform.right;

            forward.y = 0f;
            right.y = 0f;

            forward.Normalize();
            right.Normalize();

            desiredMoveDirection = forward * InputZ + right * InputX;

            if (!blockRotationPlayer)
            {
                //朝向摄像机前方
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(desiredMoveDirection), desiredRotationSpeed);
                controller.Move(desiredMoveDirection * Time.deltaTime * Velocity);
            }
        }

        private void InputMagnitude()
        {
            InputX = Input.GetAxis("Horizontal");
            InputZ = Input.GetAxis("Vertical");

            Speed = new Vector2(InputX, InputZ).sqrMagnitude;

            if (Speed > allowPlayerRotation)
            {
                PlayerMoveAndRotation();
            }
        }
    }
}
