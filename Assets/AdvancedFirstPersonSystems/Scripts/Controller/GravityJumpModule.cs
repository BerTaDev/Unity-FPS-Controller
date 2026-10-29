using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BerTaDEV
{
    [System.Serializable]
    public class GravityJumpModule
    {
        public bool isActive;
        [Header("Gravity and Jump Settings")]
        public bool isGrounded;
        public float groundCheckDistance = 0.1f;
        public LayerMask groundLayer;
        [Space]
        public float gravity = -9.81f;
        public float jumpHeight = 2f;
        [Space]
        public bool enableLandShake = true;
        public ShakePreset landShakePreset;

        private AdvancedFirstPerson AFP;
        private Vector3 velocity;
        private bool inAir;
        public void Init(AdvancedFirstPerson afp)
        {
            AFP = afp;
        }
        public void GroundCheck()
        {
            Vector3 rayStart = AFP.characterController.transform.position;
            Debug.DrawRay(rayStart, Vector3.down * groundCheckDistance, Color.red);
            isGrounded = Physics.Raycast(rayStart, Vector3.down, out RaycastHit hit, groundCheckDistance, groundLayer);

            if (!isGrounded && !inAir)
            {
                inAir = true;
            }
            else if (isGrounded && inAir)
            {
                inAir = false;
                AFP.footStepSoundsModule.PlayLandFootstepSound();
                if (enableLandShake)
                    AFP.CameraShaker.PlayShake(landShakePreset);
            }
        }
        public void HandleGravityAndJump()
        {
            if (!isActive) return;

            if ((AFP.climbLadderModule != null) && AFP.climbLadderModule.isActive && AFP.climbLadderModule.GetIsOnStairs)
            {
                Vector3 climbDirection = new Vector3(0.0f, AFP.movementModule.vertical, 0.0f).normalized;
                velocity = climbDirection * AFP.climbLadderModule.climbSpeed;
            }
            else
            {
                if (isGrounded && velocity.y < 0)
                {
                    velocity.y = -2f;
                }

                if (Input.GetButtonDown("Jump") && isGrounded && !AFP.slidingModule.getIsSliding && AFP.crouchModule.CanStandUp())
                {
                    velocity.y = Mathf.Sqrt((AFP.crouchModule.isCrouch ? jumpHeight / 1.5f : jumpHeight) * -2f * gravity);
                    AFP.footStepSoundsModule.PlayJumpFootstepSound();
                }

                velocity.y += gravity * Time.deltaTime;
            }

            AFP.characterController.Move(velocity * Time.deltaTime);
        }
    }
}
