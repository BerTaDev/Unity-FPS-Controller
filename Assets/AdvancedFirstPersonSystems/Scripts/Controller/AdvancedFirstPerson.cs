using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BerTaDEV
{
    [RequireComponent(typeof(CharacterController))]
    public class AdvancedFirstPerson : MonoBehaviour
    {
        [Header("Debug")]
        public bool drawGizmos;
        public Mesh controllerGizmosMesh;
        public Material transparentMaterial;

        [Header("Cursor")]
        public bool ActiveCursor;

        [Header("Components")]
        public Animator cameraAnimator;
        public Transform leanTransform;
        public Transform cameraTransform;
        public Transform movementFxTransform;
        public Shaker CameraShaker;

        public CharacterController characterController;

        [Header("Modules")]
        public MovementModule movementModule;
        public LookModule lookModule;
        public CameraAnimationModule cameraAnimationModule;
        public GravityJumpModule gravityJumpModule;
        public ClimbLadderModule climbLadderModule;
        public CrouchModule crouchModule;
        public SlidingModule slidingModule;
        public LeaningModule leaningModule;
        public MovementFxModule movementFxModule;
        public FootStepSoundsModule footStepSoundsModule;
        public PushModule pushModule;

        private void Start()
        {
            Cursor.visible = ActiveCursor;
            Cursor.lockState = ActiveCursor ? CursorLockMode.None : CursorLockMode.Locked;

            // Initialize Modules
            movementModule.Init(this);
            lookModule.Init(this);
            cameraAnimationModule.Init(this);
            gravityJumpModule.Init(this);
            climbLadderModule.Init(this);
            crouchModule.Init(this);
            slidingModule.Init(this);
            leaningModule.Init(this);
            movementFxModule.Init(this);
            footStepSoundsModule.Init(this);
        }
        private void OnValidate()
        {
            Cursor.visible = ActiveCursor;
            Cursor.lockState = ActiveCursor ? CursorLockMode.None : CursorLockMode.Locked;
        }
        private void Update()
        {
            movementModule.HandleMovement();
            cameraAnimationModule.HandleAnimations();
            gravityJumpModule.GroundCheck();
            gravityJumpModule.HandleGravityAndJump();
            climbLadderModule.HandleChecks();
            crouchModule.HandleCrouch();
            slidingModule.HandleSliding();
            leaningModule.HandleLeaning();
            movementFxModule.HandleFX();
            footStepSoundsModule.HandleFootStepSounds();
        }
        private void LateUpdate()
        {
            lookModule.HandleLook();
        }
        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            if (!pushModule.isActive) return;

            Rigidbody rb = hit.collider.GetComponent<Rigidbody>();
            if (rb != null)
            {
                // Çarpma yönünü bul
                Vector3 pushDirection = hit.transform.position - transform.position;
                pushDirection.Normalize(); // Yönü normalize et

                // Objeye itme kuvveti uygula
                rb.AddForce(pushDirection * pushModule.pushStrength, ForceMode.Impulse);
            }
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (!drawGizmos) return;

            if (controllerGizmosMesh && transparentMaterial != null)
            {
                Gizmos.color = Color.green;
                transparentMaterial.SetPass(0);
                Graphics.DrawMeshNow(controllerGizmosMesh, transform.position + new Vector3(0.0f, 1.0f, 0.0f), Quaternion.identity);
            }

            GUIStyle AFPTitle = new GUIStyle();
            AFPTitle.normal.textColor = Color.black;
            AFPTitle.fontSize = 20;
            AFPTitle.fontStyle = FontStyle.Bold;
            AFPTitle.alignment = TextAnchor.MiddleCenter;
            UnityEditor.Handles.Label(transform.position + Vector3.up * 2.25f, $"AFP", AFPTitle);
        }
        private void OnDrawGizmosSelected()
        {
            if (!drawGizmos) return;

            GUIStyle isGroundedStyle = new GUIStyle();
            isGroundedStyle.normal.textColor = Color.black;
            isGroundedStyle.fontSize = 10;
            isGroundedStyle.fontStyle = FontStyle.Bold;
            UnityEditor.Handles.Label(transform.position + Vector3.up * 0.5f, $"isGrounded={gravityJumpModule.isGrounded}", isGroundedStyle);

            Gizmos.color = Color.blue;
            Vector3 leanPoint = leanTransform.position;
            Gizmos.DrawSphere(leanPoint, 0.1f);
            GUIStyle leanStyle = new GUIStyle();
            leanStyle.normal.textColor = Color.black;
            leanStyle.fontSize = 10;
            leanStyle.fontStyle = FontStyle.Bold;
            UnityEditor.Handles.Label(leanPoint + Vector3.up * 0.1f, $"Leaning={leaningModule.isActive}", leanStyle);
            UnityEditor.Handles.Label(leanPoint + Vector3.up * 0.25f, $"LeanValue={leaningModule.GetCurrentLeanAngle}", leanStyle);

            Gizmos.color = Color.yellow;
            Vector3 moveFxPoint = movementFxTransform.position;
            Gizmos.DrawSphere(moveFxPoint, 0.1f);
            GUIStyle moveFxStyle = new GUIStyle();
            moveFxStyle.normal.textColor = Color.black;
            moveFxStyle.fontSize = 10;
            moveFxStyle.fontStyle = FontStyle.Bold;
            UnityEditor.Handles.Label(moveFxPoint, $"MoveFX={movementFxModule.isActive}", moveFxStyle);

            Gizmos.color = Color.red;
            Vector3 footStepPoint = footStepSoundsModule.footstepAudioSource.transform.position;
            Gizmos.DrawSphere(footStepPoint, 0.1f);
            GUIStyle footStepStyle = new GUIStyle();
            footStepStyle.normal.textColor = Color.black;
            footStepStyle.fontSize = 10;
            footStepStyle.fontStyle = FontStyle.Bold;
            UnityEditor.Handles.Label(footStepPoint, $"FootStepSFX={footStepSoundsModule.isActive}", footStepStyle);
        }
#endif
    }

}
