using UnityEngine;

using GlobalGameJam.Audio;

namespace GlobalGameJam.Gameplay.States
{
    public class IntroState : BaseState
    {
        public override GameplayStateEnum State => GameplayStateEnum.Intro;

        [Header("Duration")]
        [SerializeField, Range(0.0f, 15.0f)] private float _introSequenceLenght = 1.0f;
        private float _introTimer = 0.0f;

        [Header("Scene references")]
        [SerializeField] private Animator _comedian = null;
        [SerializeField] private AudioClip _music = null;

        protected override void EnableState()
        {
            _introTimer = 0.0f;

            AudioManager.Instance.StartMusic(_music);
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