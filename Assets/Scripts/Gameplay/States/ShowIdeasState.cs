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
        [SerializeField] private GameObject _ideasParent = null;
        [SerializeField] private int _minIdeaCount = 5;
        [SerializeField] private int _maxIdeaCount = 10;

        private List<IdeaBubble> _ideaBubbles = new List<IdeaBubble>();

        protected override void InitializeState()
        {
            _ideaBubbles.AddRange(_ideasParent.GetComponentsInChildren<IdeaBubble>(true));
        }

        protected override void EnableState()
        {
            _timer = 0.0f;

            List<IdeaData> ideaList = new List<IdeaData>(DatabaseManager.Instance.Ideas);
            int _ideaAmount = Random.Range(_minIdeaCount, _maxIdeaCount + 1);

            List<IdeaBubble> ideaBubbles = new List<IdeaBubble>(_ideaBubbles);
            for (int i = 0; i < _ideaAmount; i++)
            {
                IdeaData ideaData = ideaList[Random.Range(0, ideaList.Count)];
                IdeaBubble ideaBubble = ideaBubbles[Random.Range(0, ideaBubbles.Count)];

                ideaList.Remove(ideaData);
                ideaBubbles.Remove(ideaBubble);

                ideaBubble.SetIdea(ideaData);
            }
        }

        protected override void DisableState()
        {
            for (int i = 0; i < _ideaBubbles.Count; i++)
            {
                _ideaBubbles[i].gameObject.SetActive(false);
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