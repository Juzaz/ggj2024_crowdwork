using System;
using System.Collections;
using UnityEngine;

namespace GlobalGameJam.Gameplay.States
{
    public class BaseState : MonoBehaviour, IInitializable
    {
        public GameplayStateEnum State = GameplayStateEnum.Intro;

        protected event Action OnCompleted;

        public IEnumerator Initialize()
        {
            InitializeState();
            yield break;
        }
        
        protected virtual void InitializeState()
        {

        }

        public void StartState(Action onComplete)
        {
            OnCompleted = onComplete;

            gameObject.SetActive(true);
        }

        public void EndState()
        {
            if (!gameObject.activeSelf) return;

            gameObject.SetActive(false);

            OnCompleted?.Invoke();
            OnCompleted = null;
        }
    }
}