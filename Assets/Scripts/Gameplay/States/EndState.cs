using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace GlobalGameJam.Gameplay.States
{
    public class EndState : BaseState
    {
        public override GameplayStateEnum State => GameplayStateEnum.EndState;

        [Header("Duration")]
        [SerializeField, Range(0.0f, 15.0f)] private float _introSequenceLenght = 1.0f;
        private float _introTimer = 0.0f;

        [Header("Scene references")]
        [SerializeField] private Animator _comedian = null;
        [SerializeField] private Animator _curtain = null;
        [SerializeField] private GameObject _audienceParent = null;
        [SerializeField] private GameObject _finalScorePopup = null;
        [SerializeField] private TMP_Text _finalScoreText = null;

        [SerializeField] private AudioClip[] _happyAudience = null;
        [SerializeField] private AudioClip[] _neutralAudience = null;
        [SerializeField] private AudioClip[] _negativeAudience = null;

        List<AudienceMember> _audienceList = new List<AudienceMember>();
        private bool _endGame = false;
        private bool _triggerCurtain = false;

        protected override void InitializeState()
        {
            _audienceList.AddRange(_audienceParent.GetComponentsInChildren<AudienceMember>());
        }

        protected override void EnableState()
        {
            _introTimer = 0.0f;
            _endGame = _gameplayManager.EndGame();

            if (_endGame)
            {
                _triggerCurtain = true;
                _introTimer = 2.5f;
                int _finalScore = _gameplayManager.FinalScore;
                _finalScoreText.SetText(_finalScore.ToString());

                if (_finalScore <= 0)
                {
                    _comedian.SetTrigger("Death");
                    Audio.AudioManager.Instance.PlaySFX(_negativeAudience[0]);
                    Audio.AudioManager.Instance.PlaySFX(_negativeAudience[1]);

                    for (int i = 0; i < _audienceList.Count; i++)
                    {
                        _audienceList[i].Boo();
                    }
                }
                else if (_finalScore > 3)
                {
                    _comedian.SetTrigger("Victory");
                    Audio.AudioManager.Instance.PlaySFX(_happyAudience[0]);
                    Audio.AudioManager.Instance.PlaySFX(_happyAudience[1]);

                    for (int i = 0; i < _audienceList.Count; i++)
                    {
                        _audienceList[i].Laugh();
                    }
                }
                else
                {
                    _comedian.SetTrigger("Neutral");
                    Audio.AudioManager.Instance.PlaySFX(_neutralAudience[0]);
                    Audio.AudioManager.Instance.PlaySFX(_neutralAudience[1]);

                    for (int i = 0; i < _audienceList.Count; i++)
                    {
                        _audienceList[i].ResetState();
                    }
                }
            }
            else
            {
                _gameplayManager.IncrementRound();
            }
        }

        protected override void DisableState()
        {
        }

        public void Button_Restart()
        {
            _finalScorePopup.SetActive(false);
            _comedian.SetTrigger("Reset");

            _endGame = false;
            _curtain.SetTrigger("Raise");

            for (int i = 0; i < _audienceList.Count; i++)
            {
                _audienceList[i].ResetState();
            }

            _gameplayManager.ResetGame();
            _introTimer = -5.0f;
        }

        private void Update()
        {
            if (_endGame)
            {
                if (_triggerCurtain)
                {
                    _introTimer -= Time.deltaTime;
                    if (_introTimer <= 0.0f)
                    {
                        _triggerCurtain = false;
                        _curtain.SetTrigger("Lower");

                        for (int i = 0; i < _audienceList.Count; i++)
                        {
                            _audienceList[i].ResetState();
                        }

                        _finalScorePopup.SetActive(true);
                    }
                }
                return;
            }

            _introTimer += Time.deltaTime;
            if (_introTimer >= _introSequenceLenght)
            {
                EndState();
            }
        }
    }
}