using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GlobalGameJam.Gameplay.States
{
    public class ThrowAndCollectState : BaseState
    {
        public override GameplayStateEnum State => GameplayStateEnum.ThrowAndCollect;

        [Header("References")]
        [SerializeField] private GameObject _ideasParent = null;
        [SerializeField] private Animator _comedianAnimator = null;
        [SerializeField] private Microphone _microphone = null;
        [SerializeField] private ReturningMicrophone _returningMicrophone = null;
        [SerializeField] private AudioClip _throwMicrophone = null;

        private List<IdeaBubble> _ideaBubbles = new List<IdeaBubble>();


        protected override void InitializeState()
        {
            _ideaBubbles.AddRange(_ideasParent.GetComponentsInChildren<IdeaBubble>(true));
        }

        protected override void EnableState()
        {
            _comedianAnimator.SetTrigger("Throw");
            _microphone.gameObject.SetActive(true);
            Audio.AudioManager.Instance.PlaySFX(_throwMicrophone, 0.1f);
        }

        protected override void DisableState()
        {
            for (int i = 0; i < _ideaBubbles.Count; i++)
            {
                _ideaBubbles[i].gameObject.SetActive(false);
            }
        }

        private void Update()
        {
            if (!_microphone.gameObject.activeSelf)
            {
                _returningMicrophone.gameObject.SetActive(true);
                EndState();
            }
        }
    }
}