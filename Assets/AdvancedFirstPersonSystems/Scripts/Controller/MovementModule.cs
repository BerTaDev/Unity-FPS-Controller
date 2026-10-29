using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BerTaDEV
{
    [System.Serializable]
    public class MovementModule
    {
        public bool isActive;

        [Header("Movement Settings")]
        public float walkSpeed = 5f;
        public float sprintSpeed = 8f;
        [Header("Smooth Movement")]
        public bool enableSmoothing;
        public float smoothValue = 5f;
        [Header("Realistic Run")]
        public bool enableRealisticRun;
        [Space]
        [ReadOnlyInspector, Range(-1.0f, 1.0f)] public float horizontal;
        [ReadOnlyInspector, Range(-1.0f, 1.0f)] public float vertical;

        private bool walk;
        private bool sprint;
        private float targetSpeed;
        private float lerpedTargetSpeed;

        private AdvancedFirstPerson AFP;
        private Vector3 moveDirection;
        private Vector3 lerpedMoveDirection;
        private Vector3 inputDirection;
        
        public bool isWalk { get { return walk; } }
        public bool isSprint { get { return sprint && enableSmoothing ? lerpedTargetSpeed >= (sprintSpeed - 0.25f) : targetSpeed >= (sprintSpeed - 0.25f); } }
        public void Init(AdvancedFirstPerson afp)
        {
            AFP = afp;
        }
        public void HandleMovement()
        {
            if (!isActive) return;

            horizontal = Input.GetAxis("Horizontal");
            vertical = Input.GetAxis("Vertical");

            inputDirection = (AFP.climbLadderModule != null) && AFP.climbLadderModule.isActive && AFP.climbLadderModule.GetIsOnStairs && !AFP.gravityJumpModule.isGrounded ?
                new Vector3(horizontal, 0, 0).normalized :

                new Vector3(horizontal, 0, vertical).normalized;

            walk = inputDirection != Vector3.zero;
            sprint = (enableRealisticRun ? vertical > 0.1f : true) && Input.GetKey(KeyCode.LeftShift) && !AFP.crouchModule.isCrouch && !AFP.climbLadderModule.GetIsOnStairs;

            targetSpeed = sprint ? sprintSpeed : AFP.crouchModule.isCrouch ? walkSpeed / 2 : walkSpeed;
            lerpedTargetSpeed = Mathf.Lerp(lerpedTargetSpeed, targetSpeed, smoothValue * Time.deltaTime);

            if (inputDirection.magnitude >= 0.1f)
            {
                float speed = enableSmoothing ? lerpedTargetSpeed : targetSpeed;
                moveDirection = inputDirection * speed;

                moveDirection = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, 0) * moveDirection;
            }
            else
            {
                moveDirection = Vector3.zero;
            }
            lerpedMoveDirection = Vector3.Lerp(lerpedMoveDirection, moveDirection, smoothValue * Time.deltaTime);
            Vector3 dir = enableSmoothing ? lerpedMoveDirection : moveDirection;
            AFP.characterController.Move(dir * Time.deltaTime);
        }
    }
}
