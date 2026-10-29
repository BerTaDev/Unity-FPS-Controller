using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BerTaDEV
{
    [System.Serializable]
    public class LeaningModule
    {
        public bool isActive;
        [Header("Leaning Settings")]
        public float leanAngle = 15f;
        public float leanSpeed = 5f;

        private AdvancedFirstPerson AFP;
        private float currentLeanAngle;
        public float GetCurrentLeanAngle { get { return currentLeanAngle; } }
        public void Init(AdvancedFirstPerson afp)
        {
            AFP = afp;
        }

        public void HandleLeaning()
        {
            if (!isActive) return;
            float targetLean = 0f;

            if (Input.GetKey(KeyCode.Q))
            {
                targetLean = leanAngle;
            }
            else if (Input.GetKey(KeyCode.E))
            {
                targetLean = -leanAngle;
            }

            currentLeanAngle = Mathf.Lerp(currentLeanAngle, targetLean, Time.deltaTime * leanSpeed);
            AFP.leanTransform.localRotation = Quaternion.Euler(0, 0, currentLeanAngle);
        }
    }
}
