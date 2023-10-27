using UnityEngine;

namespace _Skycall.Scripts.Audio
{
    public class CoinCollectedAudio : AudioElement
    {
        [SerializeField] private AudioClip audioClip;

        public override AudioClip AudioClip
        {
            get => audioClip;
            set => audioClip = value;
        }
    }
}