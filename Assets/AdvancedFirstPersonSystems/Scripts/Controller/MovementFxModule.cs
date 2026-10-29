using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BerTaDEV
{
    [System.Serializable]
    public class MovementFxModule
    {
        public bool isActive;
        [Header("Procedural Settings")]
        [Range(0.05f, 2)] public float rotationAmount = 0.2f;
        [Range(1f, 20)] public float rotationSmooth = 6f;
        [Header("Movement")]
        [Range(0.1f, 2)] public float movementAmount = 0.5f;

        private AdvancedFirstPerson AFP;
        Quaternion installRotation;
        Vector3 movementVector;

        public void Init(AdvancedFirstPerson afp)
        {
            AFP = afp;
            installRotation = AFP.movementFxTransform.localRotation;
        }
        public void HandleFX()
        {
            float movementX = (AFP.movementModule.vertical * rotationAmount);
            float movementZ = (-AFP.movementModule.horizontal * rotationAmount);
            movementVector = new Vector3(isActive ? movementX + AFP.characterController.velocity.y * movementAmount : isActive ? movementX : 0.0f, 0, isActive ? movementZ : 0.0f);
            AFP.movementFxTransform.localRotation = Quaternion.Lerp(AFP.movementFxTransform.localRotation, Quaternion.Euler(movementVector + installRotation.eulerAngles), Time.deltaTime * rotationSmooth);
        }
    }
}
