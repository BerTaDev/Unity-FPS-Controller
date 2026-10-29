using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BerTaDEV
{
    [System.Serializable]
    public class ClimbLadderModule
    {
        public bool isActive;

        [Header("Climb Settings")]
        public float climbSpeed = 3f; // Merdiven tırmanma hızı

        [Header("Stair Detection")]
        public LayerMask climbableLayers;
        public float stairDetectionDistance = 1f; // Merdiveni algılama mesafesi

        private bool isOnStairs = false;
        private AdvancedFirstPerson AFP;

        public bool GetIsOnStairs { get { return isOnStairs; } }

        public void Init(AdvancedFirstPerson afp)
        {
            AFP = afp;
        }

        public void HandleChecks()
        {
            // Karakterin önüne bir ray göndererek merdiveni algılar
            Vector3 rayOrigin = AFP.characterController.transform.position + Vector3.up * (AFP.characterController.height / 2);
            isOnStairs = Physics.Raycast(rayOrigin, AFP.characterController.transform.forward, stairDetectionDistance, climbableLayers);
            // Debug Ray
            Debug.DrawRay(rayOrigin, AFP.characterController.transform.forward * stairDetectionDistance, isOnStairs ? Color.green : Color.red);
        }
    }
}
