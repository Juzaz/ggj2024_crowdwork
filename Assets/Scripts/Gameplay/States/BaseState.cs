using System;
using System.Collections;
using UnityEngine;

namespace GlobalGameJam.Gameplay.States
{
    public class BaseState : MonoBehaviour, IInitializable
    {
        public virtual GameplayStateEnum State => GameplayStateEnum.Intro;

        protected event Action OnCompleted;

        public IEnumerator Initialize()
        {
            InitializeState();

            gameObject.SetActive(false);
            yield break;
        }
        
        protected virtual void InitializeState()
        {

        }

        public void StartState(Action onComplete)
        {
            Debug.Log($"Starting {State}");
            OnCompleted = onComplete;

            gameObject.SetActive(true);
        }

        public void EndState()
        {
            if (!gameObject.activeSelf) return;

            Debug.Log($"Ending {State}");
            gameObject.SetActive(false);

            OnCompleted?.Invoke();
            OnCompleted = null;
        }
    }
}