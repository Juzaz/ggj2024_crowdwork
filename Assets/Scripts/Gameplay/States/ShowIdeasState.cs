using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GlobalGameJam.Data;

namespace GlobalGameJam.Gameplay.States
{
    public class ShowIdeasState : BaseState
    {
        public override GameplayStateEnum State => GameplayStateEnum.ShowIdeas;

        [Header("Duration")]
        [SerializeField, Range(0.0f, 15.0f)] private float _sequenceLenght = 1.0f;
        private float _timer = 0.0f;

        [Header("Ideas")]
        [SerializeField] private int _minIdeaCount = 5;
        [SerializeField] private int _maxIdeaCount = 10;

        protected override void EnableState()
        {
            _timer = 0.0f;

            List<IdeaData> ideaList = DatabaseManager.Instance.Ideas;
            int _ideaAmount = Random.Range(_minIdeaCount, _maxIdeaCount + 1);

            for (int i = 0; i < _ideaAmount; i++)
            {

            }
        }

        private void Update()
        {
            _timer += Time.deltaTime;

            if (_timer >= _sequenceLenght)
            {
                EndState();
            }
        }
    }
}