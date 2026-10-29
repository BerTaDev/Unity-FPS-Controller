using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BerTaDEV
{
    [System.Serializable]
    public class LookModule
    {
        public bool isActive;
        [Header("Look Settings")]
        [Range(1.0f, 10.0f)] public float sensitivity = 1500f;
        [Range(20.0f, 100.0f)] public float xClamp = 90f;

        private AdvancedFirstPerson AFP;
        private float xRotation = 0f;

        public void Init(AdvancedFirstPerson afp)
        {
            AFP = afp;
        }

        public void HandleLook()
        {
            if (!isActive) return;
            float mouseX = Input.GetAxis("Mouse X") * sensitivity;
            float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -xClamp, xClamp); // Yukarı-aşağı sınırlama

            AFP.cameraTransform.localRotation = Quaternion.Euler(xRotation, 0, 0);
            AFP.characterController.transform.Rotate(Vector3.up * mouseX); // Sağa-sola dönüş
        }
    }
}
