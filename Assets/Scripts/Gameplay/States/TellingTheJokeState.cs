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
        bool _failed = false;

        protected override void InitializeState()
        {
            _jokeText.transform.parent.gameObject.SetActive(false);
        }

        protected override void EnableState()
        {
            _drumrollClip = null;
            _jokeData = null;
            _jokeToTell = string.Empty;
            _failed = false;

            IdeaData[] ideas = microphone.CollectedIdeas;
            int jokeIdeaCount = Mathf.Min(4, ideas.Length);

            List<JokeData> jokes = DatabaseManager.Instance.GetJokesWithIdeaCount(jokeIdeaCount);
            _drumrollLenght = 0.55f;

            if (jokes.Count != 0)
            {
                _drumrollClip = _drumrolls[Random.Range(0, _drumrolls.Length)];
                
                AudioManager.Instance.PlaySFX(_drumrollClip);

                _jokeData = jokes[Random.Range(0, jokes.Count)];
                _jokeToTell = _jokeData.GetJoke(ideas);
            }
            else
            {
                _failed = true;
            }
        }

        protected override void DisableState()
        {
            _jokeText.transform.parent.gameObject.SetActive(false);
        }

        private void Update()
        {
            _drumrollLenght -= Time.deltaTime;
            if (_drumrollLenght > 0.0f) return;

            if (_jokeData != null)
            {
                _jokeText.SetText(_jokeToTell);
                _jokeText.transform.parent.gameObject.SetActive(true);

                _comicAnimator.SetTrigger("Joke");

                AudioClip joke = _jokeData.Audio != null ? _jokeData.Audio : _defaultJoke;
                AudioManager.Instance.PlaySFX(_jokeData.Audio);
                _timeToEnd = _jokeData.Audio.length;

                _gameplayManager.SetToldJoke(_jokeData, microphone.CollectedIdeas);
                _jokeData = null;
                return;
            }

            if (_failed)
            {
                _failed = false;
                _comicAnimator.SetTrigger("Missed");

                AudioManager.Instance.PlaySFX(_missedJoke);
                _timeToEnd = _missedJoke.length;
            }

            _timeToEnd -= Time.deltaTime;
            if (_timeToEnd <= 0.0f)
            {
                EndState();
            }
        }
    }
}