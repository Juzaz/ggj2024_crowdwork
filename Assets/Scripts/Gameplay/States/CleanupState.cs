using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GlobalGameJam.Gameplay.States
{
    public class CleanupState : BaseState
    {
        public override GameplayStateEnum State => GameplayStateEnum.Cleanup;

        [Header("Duration")]
        [SerializeField, Range(0.0f, 15.0f)] private float _sequenceLenght = 1.0f;
        private float _timer = 0.0f;

        [Header("References")]
        [SerializeField] Animator _comedian = null;

        protected override void EnableState()
        {
            _timer = 0.0f;

            _comedian.SetTrigger("Reset");
        }

        protected override void DisableState()
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