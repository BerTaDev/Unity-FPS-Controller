using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BerTaDEV
{
    [System.Serializable]
    public class CrouchModule
    {
        public bool isActive;
        [Header("Crouch Settings")]
        public bool isCrouch;
        public float crouchHeight = 1f;
        public float normalHeight = 2f;
        public float crouchCameraHeight = 1f;

        public float transitionSpeed = 5f;

        AdvancedFirstPerson AFP;
        private float targetHeight;
        private float targetCameraHeight;
        private float cameraPivotAwakeHeight;

        public void Init(AdvancedFirstPerson afp)
        {
            AFP = afp;
            targetHeight = normalHeight;
            cameraPivotAwakeHeight = AFP.cameraTransform.transform.localPosition.y;
        }

        public void HandleCrouch()
        {
            if (!isActive) return;

            if (Input.GetKeyDown(KeyCode.LeftControl) || AFP.slidingModule.getIsSliding)
            {
                isCrouch = true;
            }
            if (Input.GetKeyUp(KeyCode.LeftControl) && !AFP.slidingModule.getIsSliding)
            {
                if (CanStandUp())
                {
                    isCrouch = false;
                }
            }
            if (!Input.GetKey(KeyCode.LeftControl) && CanStandUp() && !AFP.slidingModule.getIsSliding)
            {
                isCrouch = false;
            }

            targetHeight = isCrouch ? crouchHeight : normalHeight;
            AFP.characterController.height = Mathf.Lerp(AFP.characterController.height, targetHeight, Time.deltaTime * transitionSpeed);

            targetCameraHeight = isCrouch ? crouchCameraHeight : cameraPivotAwakeHeight;
            AFP.cameraTransform.localPosition = Vector3.Lerp(AFP.cameraTransform.localPosition, new Vector3(0.0f, targetCameraHeight, 0.0f), transitionSpeed * Time.deltaTime);

            Vector3 center = AFP.characterController.center;
            center.y = AFP.characterController.height / 2;
            AFP.characterController.center = center;
        }

        public bool CanStandUp()
        {
            float headClearance = normalHeight - crouchHeight;
            Vector3 rayStart = AFP.characterController.transform.position + Vector3.up * crouchHeight;
            float rayLength = headClearance;

            // Raycast sonucunu doğrudan kontrol edin
            bool hitSomething = Physics.Raycast(rayStart, Vector3.up, out RaycastHit hit, rayLength);

            // Debug ray çizimi
            Debug.DrawRay(rayStart, Vector3.up * rayLength, hitSomething ? Color.red : Color.green);

            // Eğer bir nesneye çarptıysa kalkılamaz
            return !hitSomething;
        }
    }
}
