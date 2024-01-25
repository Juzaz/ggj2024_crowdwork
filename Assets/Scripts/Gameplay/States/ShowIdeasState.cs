using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GlobalGameJam.Audio;
using GlobalGameJam.Data;

namespace GlobalGameJam.Gameplay.States
{
    public class ShowIdeasState : BaseState
    {
        public override GameplayStateEnum State => GameplayStateEnum.ShowIdeas;

        [Header("Duration")]
        [SerializeField, Range(0.0f, 15.0f)] private float _sequenceLenght = 1.0f;
        private float _timer = 0.0f;
        private bool _sequenceWait = true;

        [Header("Ideas")]
        [SerializeField] private GameObject _ideasParent = null;
        [SerializeField] private int _minIdeaCount = 5;
        [SerializeField] private int _maxIdeaCount = 10;

        [Header("References")]
        [SerializeField] private Animator _comicAnimator = null;
        [SerializeField] private Microphone _microphone = null;
        [SerializeField] private AngleAndForceControls forceControls = null;

        [SerializeField] private float _microphoneSpinDelay = 0.1f;
        [SerializeField] private float _microphoneSpinPitchRange = 0.1f;
        [SerializeField] private AudioClip _microphoneSpinAudio = null;
        private float _microphoneSpinTimer = 0.0f;

        private List<IdeaBubble> _ideaBubbles = new List<IdeaBubble>();

        protected override void InitializeState()
        {
            _ideaBubbles.AddRange(_ideasParent.GetComponentsInChildren<IdeaBubble>(true));
        }

        protected override void EnableState()
        {
            _timer = 0.0f;

            List<IdeaData> ideaList = new List<IdeaData>(DatabaseManager.Instance.Ideas);
            _maxIdeaCount = Mathf.Min(_maxIdeaCount, ideaList.Count);

            int _ideaAmount = Random.Range(_minIdeaCount, _maxIdeaCount + 1);

            List<IdeaBubble> ideaBubbles = new List<IdeaBubble>(_ideaBubbles);
            for (int i = 0; i < _ideaAmount; i++)
            {
                IdeaData ideaData = ideaList[Random.Range(0, ideaList.Count)];
                IdeaBubble ideaBubble = ideaBubbles[Random.Range(0, ideaBubbles.Count)];

                ideaList.Remove(ideaData);
                ideaBubbles.Remove(ideaBubble);

                ideaBubble.SetIdea(ideaData);
            }

            _sequenceWait = true;
        }

        protected override void DisableState()
        {
            _microphone.SetAngleAndForce(forceControls.Angle, forceControls.Force);
        }

        private void Update()
        {
            if (_sequenceWait)
            {
                _timer += Time.deltaTime;
                if (_timer >= _sequenceLenght)
                {
                    forceControls.gameObject.SetActive(true);
                    _sequenceWait = false;
                    _comicAnimator.SetTrigger("Spinning");
                }
            }
            else
            {
                _microphoneSpinTimer -= Time.deltaTime;

                if (_microphoneSpinTimer <= 0.0f)
                {
                    _microphoneSpinTimer = _microphoneSpinDelay;
                    AudioManager.Instance.PlaySFX(_microphoneSpinAudio, _microphoneSpinPitchRange);
                }

                if (!forceControls.gameObject.activeSelf)
                {
                    EndState();
                }
            }
        }
    }
}