using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

namespace GlobalGameJam.Audio
{
    public class AudioManager : MonoBehaviour, IInitializable
    {
        public static AudioManager Instance { get; private set; }

        [SerializeField] private AudioMixer _masterMixer = null;
        [SerializeField] private AudioSource _musicSource = null;
        [SerializeField] private AudioSource[] _sfxSources = null;

        private int _sfxSourceIndex = 0;

        public IEnumerator Initialize()
        {
            Instance = this;
            yield return null;
        }

        public void StartMusic(AudioClip audioClip)
        {
            _musicSource.clip = audioClip;
            _musicSource.Play();
        }

        public void ToggleMusicPauseState(bool paused)
        {
            if (_musicSource.clip == null) return;

            if (paused)
            {
                _musicSource.Pause();
            }
            else
            {
                _musicSource.UnPause();
            }
        }

        public void PlaySFX(AudioClip audioClip)
        {
            AudioSource audioSource = _sfxSources[_sfxSourceIndex];

            audioSource.clip = audioClip;
            audioSource.pitch = 1.0f;
            audioSource.Play();

            _sfxSourceIndex++;
            if (_sfxSourceIndex >= _sfxSources.Length -1)
            {
                _sfxSourceIndex = 0;
            }
        }

        public void PlaySFX(AudioClip audioClip, float pitchRange)
        {
            AudioSource audioSource = _sfxSources[_sfxSourceIndex];

            audioSource.clip = audioClip;
            audioSource.pitch = 1.0f + Random.Range(-pitchRange, pitchRange);
            audioSource.Play();

            _sfxSourceIndex++;
            if (_sfxSourceIndex >= _sfxSources.Length - 1)
            {
                _sfxSourceIndex = 0;
            }
        }
    }
}