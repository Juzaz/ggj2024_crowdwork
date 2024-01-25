using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GlobalGameJam.Gameplay.States
{
    public class IntroState : BaseState
    {
        public override GameplayStateEnum State => GameplayStateEnum.Intro;

        [Header("Duration")]
        [SerializeField, Range(0.0f, 15.0f)] private float _introSequenceLenght = 1.0f;
        private float _introTimer = 0.0f;

        private void OnEnable()
        {
            _introTimer = 0.0f;
        }

        private void OnDisable()
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