using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BerTaDEV
{
    [System.Serializable]
    public class CameraAnimationModule
    {
        public bool isActive;
        [Header("Animation Settings")]
        public float transitionSpeed = 5.0f;

        private float activeState;
        private AdvancedFirstPerson AFP;
        public void Init(AdvancedFirstPerson afp)
        {
            AFP = afp;
        }
        public void HandleAnimations()
        {
            activeState = isActive && AFP.crouchModule.isCrouch && AFP.movementModule.isWalk ? -1.0f : isActive && AFP.movementModule.isSprint ? 2.0f : isActive && AFP.movementModule.isWalk ? 1.0f : 0.0f;
            AFP.cameraAnimator.SetFloat("moveState", Mathf.MoveTowards(AFP.cameraAnimator.GetFloat("moveState"), activeState, transitionSpeed * Time.deltaTime));
        }
    }
}
