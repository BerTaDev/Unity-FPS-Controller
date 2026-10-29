using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BerTaDEV
{
    [System.Serializable]
    public class SlidingModule
    {
        public bool isActive;
        [Header("Sliding Settings")]
        public float slideDuration = 1f;
        public float slideSpeed = 10f;

        private float slideTimer;
        private bool isSliding;
        AdvancedFirstPerson AFP;

        public bool getIsSliding { get { return isSliding; } }
        public void Init(AdvancedFirstPerson afp)
        {
            AFP = afp;
        }

        public void HandleSliding()
        {
            if (!isActive) return;
            if (Input.GetKeyDown(KeyCode.C) && AFP.gravityJumpModule.isGrounded && !AFP.crouchModule.isCrouch && !isSliding && AFP.movementModule.isSprint)
            {
                isSliding = true;
                AFP.footStepSoundsModule.PlaySlideSound();
                slideTimer = slideDuration;
            }

            if (isSliding)
            {
                Vector3 slideDirection = AFP.characterController.transform.forward * slideSpeed; // Karakterin yönüne göre kayma
                AFP.characterController.Move(slideDirection * Time.deltaTime);

                slideTimer -= Time.deltaTime;
                if (slideTimer <= 0)
                {
                    isSliding = false;
                }
            }
        }
    }
}
