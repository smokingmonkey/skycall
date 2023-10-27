using System;
using UnityEngine;

namespace _Skycall.Scripts.Audio
{
    [Serializable]
    public abstract class AudioElement : MonoBehaviour
    {
        public abstract AudioClip AudioClip { get; set; }
    }
}