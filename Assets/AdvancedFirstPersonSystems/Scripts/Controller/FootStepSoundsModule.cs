using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BerTaDEV
{
    [System.Serializable]
    public class FootStepSoundsModule
    {
        public bool isActive;

        [Header("Footstep Settings")]
        public AudioSource footstepAudioSource; // Ayak sesi kaynağı
        public AudioClip[] walkFootsteps; // Yürüyüş için ayak sesleri
        public AudioClip[] jumpFootsteps; // Yürüyüş için ayak sesleri
        public AudioClip[] landFootsteps; // Yürüyüş için ayak sesleri
        public AudioClip slideSoundEffect; // Yürüyüş için ayak sesleri
        [Space]
        public float walkStepInterval = 0.5f; // Adımlar arasındaki süre (saniye)
        public float sprintStepInterval = 0.5f; // Adımlar arasındaki süre (saniye)

        private float stepTimer = 0f;
        private AdvancedFirstPerson AFP;
        private float getActiveInterval { get { return AFP.crouchModule.isCrouch ? walkStepInterval * 2 : AFP.movementModule.isSprint ? sprintStepInterval : walkStepInterval; } }
        public void Init(AdvancedFirstPerson afp)
        {
            AFP = afp;
        }

        public void HandleFootStepSounds()
        {
            if (!isActive) return;

            if (AFP.gravityJumpModule.isGrounded && AFP.movementModule.isWalk && !AFP.slidingModule.getIsSliding)
            {
                stepTimer += Time.deltaTime;

                // Eğer adım zamanı geldiyse bir ayak sesi çal
                if (stepTimer >= getActiveInterval)
                {
                    PlayWalkFootstepSound();
                    stepTimer = 0f;
                }
            }
            else
            {
                // Hareket yoksa adım zamanlayıcısını sıfırla
                stepTimer = 0f;
            }
        }

        private void PlayWalkFootstepSound()
        {
            if (footstepAudioSource == null) return;

            if (walkFootsteps.Length > 0)
            {
                // Rastgele bir ayak sesi seç ve çal
                AudioClip footstepClip = walkFootsteps[Random.Range(0, walkFootsteps.Length)];
                footstepAudioSource.PlayOneShot(footstepClip);
            }
        }
        public void PlayJumpFootstepSound()
        {
            if (!isActive) return;
            if (footstepAudioSource == null) return;

            if (jumpFootsteps.Length > 0)
            {
                // Rastgele bir ayak sesi seç ve çal
                AudioClip footstepClip = jumpFootsteps[Random.Range(0, jumpFootsteps.Length)];
                footstepAudioSource.PlayOneShot(footstepClip);
            }
        }
        public void PlayLandFootstepSound()
        {
            if (!isActive) return;
            if (footstepAudioSource == null) return;

            if (landFootsteps.Length > 0)
            {
                // Rastgele bir ayak sesi seç ve çal
                AudioClip footstepClip = landFootsteps[Random.Range(0, landFootsteps.Length)];
                footstepAudioSource.PlayOneShot(footstepClip);
            }
        }
        public void PlaySlideSound()
        {
            if (!isActive) return;
            if (footstepAudioSource == null) return;
            footstepAudioSource.PlayOneShot(slideSoundEffect);
        }
    }
}
