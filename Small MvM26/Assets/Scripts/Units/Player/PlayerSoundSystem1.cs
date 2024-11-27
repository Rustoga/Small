using UnityEngine;

namespace SmallGame.Player
{
    public class PlayerSoundSystem1 : MonoBehaviour
    {
        AudioSource _audioSource;
        public AudioClip AttackSound;
        public AudioClip DashSound;
        public AudioClip HitSound;
        public AudioClip JumpLandSound;
        public AudioClip JumpUpSound;
        public AudioClip RunSound;

        void Awake() {
            _audioSource = GetComponent<AudioSource>();
        }
        public void PlaySound(AudioClip audioSound, bool loop)
        {
            _audioSource.clip = audioSound;
            _audioSource.loop = loop;
            _audioSource.Play();
        }
        public void StopSound()
        {
            _audioSource.Stop();
            _audioSource.loop = false;
        }
    }
}
