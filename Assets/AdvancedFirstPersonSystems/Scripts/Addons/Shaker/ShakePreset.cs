using UnityEngine;

namespace BerTaDEV
{
    [CreateAssetMenu(fileName = "NewShakePreset", menuName = "BerTaDEV/Shaker/Shake Preset", order = 1)]
    public class ShakePreset : ScriptableObject
    {
        [Header("Position Shake")]
        public Vector3 positionStrength = Vector3.one;

        [Header("Rotation Shake")]
        public Vector3 rotationStrength = Vector3.one;

        [Header("General Settings")]
        public float Strength = 1f;
        public float Duration = 1f;
        public float FadeInDuration = 0.2f;
        public float FadeOutDuration = 0.2f;
        public float Frequency = 2f; // Sinüs frekansı
    }
}
