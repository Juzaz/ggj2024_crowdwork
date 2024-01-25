using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GlobalGameJam.Gameplay.States
{
    public class ShowIdeasState : BaseState
    {
        public override GameplayStateEnum State => GameplayStateEnum.ShowIdeas;

        [Header("Duration")]
        [SerializeField, Range(0.0f, 15.0f)] private float _sequenceLenght = 1.0f;
        private float _timer = 0.0f;

        private void OnEnable()
        {
            _timer = 0.0f;
        }

        private void OnDisable()
        {

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