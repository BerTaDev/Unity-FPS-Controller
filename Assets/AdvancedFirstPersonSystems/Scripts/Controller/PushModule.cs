using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BerTaDEV
{
    [System.Serializable]
    public class PushModule
    {
        public bool isActive;
        [Header("Push Settings")]
        public float pushStrength = 5f; // İtme kuvveti
    }
}
