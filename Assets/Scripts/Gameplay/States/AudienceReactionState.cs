using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GlobalGameJam.Audio;
using GlobalGameJam.Data;

namespace GlobalGameJam.Gameplay.States
{
    public class AudienceReactionState : BaseState
    {
        public override GameplayStateEnum State => GameplayStateEnum.AudienceReaction;

        private float _delayLenght = 1.0f;
        private float _delayTimer = 0.0f;

        [Header("Scene references")]
        [SerializeField] private Animator _comedian = null;
        [SerializeField] private GameObject _audienceParent = null;

        [Header("Audio refs")]
        
        [SerializeField] private AudioClip[] _bigGoodResponses = null;
        [SerializeField] private AudioClip[] _moderateGoodResponses = null;
        [SerializeField] private AudioClip[] _smallGoodResponses = null;

        [Space]
        [SerializeField] private AudioClip[] _neutralResponses = null;

        [Space]
        [SerializeField] private AudioClip[] _smallBadResponses = null;
        [SerializeField] private AudioClip[] _moderateBadResponses = null;
        [SerializeField] private AudioClip[] _bigBadResponses = null;

        List<AudienceMember> _audienceList = new List<AudienceMember>();

        protected override void InitializeState()
        {
            _audienceList.AddRange(_audienceParent.GetComponentsInChildren<AudienceMember>());
        }

        protected override void EnableState()
        {
            _delayTimer = 0.0f;

            JokeData currentJoke = _gameplayManager.CurrentJoke;
            IdeaData[] currentIdeas = _gameplayManager.CurrentIdeas;
            _gameplayManager.SetToldJoke(null, null);

            List<AttributeData> likedAttributes = new List<AttributeData>();
            List<AttributeData> hatedAttributes = new List<AttributeData>();
            List<AttributeData> jokeAttributes = new List<AttributeData>();
            
            for (int i = 0; i < currentIdeas.Length; i++)
            {
                jokeAttributes.AddRange(currentIdeas[i].Attributes);
            }

            for (int i = 0; i < _audienceList.Count; i++)
            {
                likedAttributes.Add(_audienceList[i].LikedAttribute);
                hatedAttributes.Add(_audienceList[i].HatedAttribute);
            }

            int score = 0;
            for (int i = 0; i < jokeAttributes.Count; i++)
            {
                for (int x = 0; x < likedAttributes.Count; x++)
                {
                    if (jokeAttributes[i] == likedAttributes[x])
                    {
                        score++;
                    }
                }

                for (int x = 0; x < hatedAttributes.Count; x++)
                {
                    if (jokeAttributes[i] == hatedAttributes[x])
                    {
                        score--;
                    }
                }
            }

            //AudioClip audioClip = null;
            //_delayLenght = audioClip.length;

            //AudioManager.Instance.PlaySFX(audioClip);
        }

        protected override void DisableState()
        {
        }

        private void Update()
        {
            _delayTimer += Time.deltaTime;

            if (_delayTimer >= _delayLenght)
            {
                EndState();
            }
        }
    }
}