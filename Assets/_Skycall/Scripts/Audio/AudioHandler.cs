using System;
using _Skycall.Scripts.Helpers;
using _Skycall.Scripts.Level.Collectibles;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Skycall.Scripts.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioHandler : MonoBehaviour
    {
        [SerializeField] private AudioSource sfxAudioSource;
        [SerializeField] private AudioSource musicAudioSource;
        [SerializeField] private AudioElement explosion;
        [SerializeField] private AudioElement coin;

        void Start()
        {
            Explosion.OnExplosion += OnPlayExplosion;
            Coin.OnCollected += OnPlayCoin;
        }

        void OnDisable()
        {
            Explosion.OnExplosion -= OnPlayExplosion;
            Coin.OnCollected += OnPlayCoin;
        }

        void OnPlayExplosion()
        {
            sfxAudioSource.PlayOneShot(explosion.AudioClip);
        }

        void OnPlayCoin(float f)
        {
            sfxAudioSource.PlayOneShot(coin.AudioClip);
        }
    }
}