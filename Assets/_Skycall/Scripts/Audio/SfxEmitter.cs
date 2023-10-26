using UnityEngine;

namespace _Skycall.Scripts.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class SfxEmitter : AudioElement
    {
        [SerializeField] private AudioClip audioClip;
        [SerializeField] private AudioSource audioSource;

        private void Start()
        {
            audioSource.loop = true;
            audioSource.clip = audioClip;
            audioSource.Play();
        }

        public override AudioClip AudioClip
        {
            get => audioClip;
            set => audioClip = value;
        }
    }
}