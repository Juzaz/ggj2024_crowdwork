using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GlobalGameJam.Audio;

namespace GlobalGameJam.Gameplay.States
{
    public class AudienceReactionState : BaseState
    {
        public override GameplayStateEnum State => GameplayStateEnum.AudienceReaction;

        private float _introSequenceLenght = 1.0f;
        private float _introTimer = 0.0f;

        [Header("Scene references")]
        [SerializeField] private Animator _comedian = null;

        [Header("Audio refs")]
        [SerializeField] private AudioClip _goodResponse = null;

        protected override void EnableState()
        {
            _introTimer = 0.0f;

            _introSequenceLenght = _goodResponse.length + 0.5f;
            AudioManager.Instance.PlaySFX(_goodResponse);
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