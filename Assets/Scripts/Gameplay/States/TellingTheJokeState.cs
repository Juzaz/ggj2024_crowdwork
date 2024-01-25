using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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

        float _timeToEnd = 1.0f;

        protected override void InitializeState()
        {
            _jokeText.gameObject.SetActive(false);
        }

        protected override void EnableState()
        {
            IdeaData[] ideas = microphone.CollectedIdeas;
            int jokeIdeaCount = ideas.Length;

            List<JokeData> jokes = DatabaseManager.Instance.GetJokesWithIdeaCount(jokeIdeaCount);

            if (jokes.Count != 0)
            {
                JokeData _joke = jokes[Random.Range(0, jokes.Count)];
                _jokeText.SetText(_joke.GetJoke(ideas));

                _jokeText.gameObject.SetActive(true);
                _comicAnimator.SetTrigger("Joke");

                _timeToEnd = 10.0f;
            }
            else
            {
                _comicAnimator.SetTrigger("Bad");

                _timeToEnd = 5.0f;
            }
            
        }

        protected override void DisableState()
        {
            _jokeText.gameObject.SetActive(false);
        }

        private void Update()
        {
            _timeToEnd -= Time.deltaTime;
            if (_timeToEnd <= 0.0f)
            {
                EndState();
            }
        }
    }
}