using System;
using System.Collections;
using UnityEngine;

namespace GlobalGameJam.Gameplay.States
{
    public class BaseState : MonoBehaviour, IInitializable
    {
        public virtual GameplayStateEnum State => GameplayStateEnum.Intro;

        protected GameplayManager _gameplayManager = null;
        protected event Action _onCompleted = null;
        protected bool _isInitialized = false;

        public IEnumerator Initialize()
        {
            InitializeState();

            gameObject.SetActive(false);
            _isInitialized = true;
            yield break;
        }

        public void SetReferences(GameplayManager gameplayManager)
        {
            _gameplayManager = gameplayManager;
        }
        
        private void OnEnable()
        {
            if (!_isInitialized) return;
            EnableState();
        }

        private void OnDisable()
        {
            if (!_isInitialized) return;
            DisableState();
        }

        public void StartState(Action onComplete)
        {
            _onCompleted = onComplete;

            gameObject.SetActive(true);
        }

        public void EndState()
        {
            if (!gameObject.activeSelf) return;

            gameObject.SetActive(false);

            _onCompleted?.Invoke();
            _onCompleted = null;
        }

        protected virtual void InitializeState() { }

        protected virtual void EnableState() { }

        protected virtual void DisableState() { }
    }
}