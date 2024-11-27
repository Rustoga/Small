using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SmallGame.Player
{
    public class PlayerSoundSystem : MonoBehaviour
    {
        [SerializeField] AudioSource _audioSource;
        public AudioClip AttackSound;
        public AudioClip DashSound;
        public AudioClip HitSound;
        public AudioClip JumpLandSound;
        public AudioClip JumpUpSound;
        public AudioClip RunSound;


        public void PlaySound(AudioClip audioSound, bool loop)
        {
            if(audioSound == null)
            {
                // Debug.LogWarning($"no audio for {audioSound}");
                return;
            }
            _audioSource.clip = audioSound;
            _audioSource.loop = loop;
            _audioSource.Play();
        }
        public void StopSound()
        {
            if(_audioSource == null) return;
            
            _audioSource.Stop();
            _audioSource.loop = false;
        }
    }
}
