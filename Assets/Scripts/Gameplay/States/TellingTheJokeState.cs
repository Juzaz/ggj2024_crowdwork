using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using GlobalGameJam.Audio;
using GlobalGameJam.Data;

namespace GlobalGameJam.Gameplay.States
{
    public class TellingTheJokeState : BaseState
    {
        public override GameplayStateEnum State => GameplayStateEnum.TellingTheJoke;

        [Header("Scene references")]
        [SerializeField] private Microphone microphone = null;
        [SerializeField] private Animator _comicAnimator = null;
        [SerializeField] private TMP_Text _jokeText = null;

        [Header("Audio")]
        [SerializeField] private AudioClip[] _drumrolls = null;
        [SerializeField] private AudioClip _missedJoke = null;
        [SerializeField] private AudioClip _defaultJoke = null;

        JokeData _jokeData = null;
        string _jokeToTell = null;
        AudioClip _drumrollClip = null;
        float _drumrollLenght = 1.0f;
        float _timeToEnd = 1.0f;

        protected override void InitializeState()
        {
            _jokeText.transform.parent.gameObject.SetActive(false);
        }

        protected override void EnableState()
        {
            _drumrollClip = null;
            _jokeData = null;
            _jokeToTell = string.Empty;

            IdeaData[] ideas = microphone.CollectedIdeas;
            int jokeIdeaCount = ideas.Length;

            List<JokeData> jokes = DatabaseManager.Instance.GetJokesWithIdeaCount(jokeIdeaCount);

            if (jokes.Count != 0)
            {
                _drumrollClip = _drumrolls[Random.Range(0, _drumrolls.Length)];
                _drumrollLenght = _drumrollClip.length + 0.25f;

                AudioManager.Instance.PlaySFX(_drumrollClip);

                _jokeData = jokes[Random.Range(0, jokes.Count)];
                _jokeToTell = _jokeData.GetJoke(ideas);
            }
            else
            {
                _comicAnimator.SetTrigger("Missed");

                AudioManager.Instance.PlaySFX(_missedJoke);
                _timeToEnd = _missedJoke.length + 0.5f;
            }
        }

        protected override void DisableState()
        {
            _jokeText.transform.parent.gameObject.SetActive(false);
        }

        private void Update()
        {
            if (_jokeData != null)
            {
                _drumrollLenght -= Time.deltaTime;
                if (_drumrollLenght > 0.0f) return;

                _jokeText.SetText(_jokeToTell);
                _jokeText.transform.parent.gameObject.SetActive(true);

                _comicAnimator.SetTrigger("Joke");

                AudioClip joke = _jokeData.Audio != null ? _jokeData.Audio : _defaultJoke;
                AudioManager.Instance.PlaySFX(_jokeData.Audio);
                _timeToEnd = _jokeData.Audio.length + 1.5f;

                _jokeData = null;
                return;
            }

            _timeToEnd -= Time.deltaTime;
            if (_timeToEnd <= 0.0f)
            {
                EndState();
            }
        }
    }
}