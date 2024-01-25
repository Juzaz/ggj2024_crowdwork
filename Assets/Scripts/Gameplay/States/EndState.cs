using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GlobalGameJam.Gameplay.States
{
    public class EndState : BaseState
    {
        public override GameplayStateEnum State => GameplayStateEnum.EndState;

        [Header("Duration")]
        [SerializeField, Range(0.0f, 15.0f)] private float _introSequenceLenght = 1.0f;
        private float _introTimer = 0.0f;

        [Header("Scene references")]
        [SerializeField] private Animator _comedian = null;

        private bool _endGame = false;

        protected override void EnableState()
        {
            _introTimer = 0.0f;
            _endGame = _gameplayManager.EndGame();

            if (_endGame)
            {

            }
            else
            {
                _gameplayManager.IncrementRound();
            }
        }

        protected override void DisableState()
        {
        }

        private void Update()
        {
            if (_endGame) return;

            _introTimer += Time.deltaTime;
            if (_introTimer >= _introSequenceLenght)
            {
                EndState();
            }
        }
    }
}