using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GlobalGameJam.Gameplay.States
{
    public class HealthAndStatusCheckState : BaseState
    {
        public override GameplayStateEnum State => GameplayStateEnum.HealthAndStatusUpdate;

        [Header("Duration")]
        [SerializeField, Range(0.0f, 15.0f)] private float _introSequenceLenght = 1.0f;
        private float _introTimer = 0.0f;

        [Header("Scene references")]
        [SerializeField] private Animator _comedian = null;

        protected override void EnableState()
        {
            _introTimer = 0.0f;
        }

        protected override void DisableState()
        {
        }

        private void Update()
        {
            _introTimer += Time.deltaTime;

            if (_introTimer >= _introSequenceLenght)
            {
                EndState();
            }
        }
    }
}