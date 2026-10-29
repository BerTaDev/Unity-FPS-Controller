using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BerTaDEV
{
    public class Shaker : MonoBehaviour
    {
        public bool m_GlobalShaker;
        private Vector3 initialPosition;
        private Quaternion initialRotation;

        private float shakeTime;
        private ShakePreset currentPreset;

        private bool isShaking;
        public static Shaker global;

        // Sinüs fazları ve frekansları
        private Vector3 positionPhase;
        private Vector3 rotationPhase;

        void Awake()
        {
            initialPosition = transform.localPosition;
            initialRotation = transform.localRotation;
            if (m_GlobalShaker)
                global = this;
        }

        public void PlayShake(ShakePreset preset)
        {
            currentPreset = preset;
            shakeTime = 0f;
            isShaking = true;

            // Sinüs fazlarını rastgele başlat
            positionPhase = new Vector3(Random.Range(0f, 2f * Mathf.PI), Random.Range(0f, 2f * Mathf.PI), Random.Range(0f, 2f * Mathf.PI));
            rotationPhase = new Vector3(Random.Range(0f, 2f * Mathf.PI), Random.Range(0f, 2f * Mathf.PI), Random.Range(0f, 2f * Mathf.PI));
        }

        void Update()
        {
            if (!isShaking || currentPreset == null) return;

            shakeTime += Time.deltaTime;
            float normalizedTime = shakeTime / currentPreset.Duration;

            // Bitmişse sarsıntıyı durdur
            if (normalizedTime >= 1f)
            {
                StopShake();
                return;
            }

            // Fade in ve fade out'u uygula
            float fadeFactor = CalculateFadeFactor(shakeTime, currentPreset.Duration, currentPreset.FadeInDuration, currentPreset.FadeOutDuration);

            // Pozisyon sarsıntısı (Rastgele + Sinüs)
            Vector3 positionShake = new Vector3(
                (Mathf.Sin((shakeTime * currentPreset.Frequency + positionPhase.x)) + Random.Range(-1f, 1f)) * currentPreset.positionStrength.x,
                (Mathf.Sin((shakeTime * currentPreset.Frequency + positionPhase.y)) + Random.Range(-1f, 1f)) * currentPreset.positionStrength.y,
                (Mathf.Sin((shakeTime * currentPreset.Frequency + positionPhase.z)) + Random.Range(-1f, 1f)) * currentPreset.positionStrength.z
            ) * currentPreset.Strength * fadeFactor;

            // Rotasyon sarsıntısı (Rastgele + Sinüs)
            Vector3 rotationShake = new Vector3(
                (Mathf.Sin((shakeTime * currentPreset.Frequency + rotationPhase.x)) + Random.Range(-1f, 1f)) * currentPreset.rotationStrength.x,
                (Mathf.Sin((shakeTime * currentPreset.Frequency + rotationPhase.y)) + Random.Range(-1f, 1f)) * currentPreset.rotationStrength.y,
                (Mathf.Sin((shakeTime * currentPreset.Frequency + rotationPhase.z)) + Random.Range(-1f, 1f)) * currentPreset.rotationStrength.z
            ) * currentPreset.Strength * fadeFactor;

            // Transform'u uygula
            transform.localPosition = initialPosition + positionShake;
            transform.localRotation = initialRotation * Quaternion.Euler(rotationShake);
        }

        public void StopShake()
        {
            isShaking = false;
            transform.localPosition = initialPosition;
            transform.localRotation = initialRotation;
        }

        private float CalculateFadeFactor(float currentTime, float totalDuration, float fadeInDuration, float fadeOutDuration)
        {
            // Fade-in bölgesi
            if (currentTime < fadeInDuration)
            {
                return currentTime / fadeInDuration;
            }
            // Fade-out bölgesi
            else if (currentTime > totalDuration - fadeOutDuration)
            {
                return Mathf.Clamp01((totalDuration - currentTime) / fadeOutDuration); // Fade-out bölgesinde yavaşça kaybolur
            }
            // Tam güçte sarsıntı
            else
            {
                return 1f;
            }
        }

    }
}
