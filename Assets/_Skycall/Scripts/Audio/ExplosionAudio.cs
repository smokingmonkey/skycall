using UnityEngine;

namespace _Skycall.Scripts.Audio
{
    public class ExplosionAudio : AudioElement
    {
        [SerializeField] private AudioClip audioClip;
        public override AudioClip AudioClip
        {
            get => audioClip;
            set => audioClip = value;
        }
    }
}