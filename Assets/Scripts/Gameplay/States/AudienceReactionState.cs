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
        [SerializeField] private AudioClip[] _tinyGoodResponses = null;

        [Space]
        [SerializeField] private AudioClip[] _neutralResponses = null;

        [Space]
        [SerializeField] private AudioClip[] _tinyBadResponses = null;
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
            AttributeData punchlineAttribute = null;

            for (int i = 0; i < currentIdeas.Length; i++)
            {
                jokeAttributes.AddRange(currentIdeas[i].Attributes);
            }
            punchlineAttribute = jokeAttributes.Count > 0 ? jokeAttributes[jokeAttributes.Count - 1] : null;

            int jokeScore = 0;
            int satisfiedAudienceMembers = 0;
            int dissatisfiedAudienceMembers = 0;

            for (int i = 0; i < _audienceList.Count; i++)
            {
                likedAttributes.Add(_audienceList[i].LikedAttribute);
                hatedAttributes.Add(_audienceList[i].HatedAttribute);

                if (punchlineAttribute == _audienceList[i].LikedAttribute)
                {
                    satisfiedAudienceMembers++;
                }
                else if (punchlineAttribute == _audienceList[i].HatedAttribute)
                {
                    dissatisfiedAudienceMembers++;
                }
            }

            for (int i = 0; i < jokeAttributes.Count; i++)
            {
                for (int x = 0; x < likedAttributes.Count; x++)
                {
                    if (jokeAttributes[i] == likedAttributes[x])
                    {
                        jokeScore++;
                    }
                }

                for (int x = 0; x < hatedAttributes.Count; x++)
                {
                    if (jokeAttributes[i] == hatedAttributes[x])
                    {
                        jokeScore--;
                    }
                }
            }

            AudioClip neutralAudio = _neutralResponses[Random.Range(0, _neutralResponses.Length)];
            AudioClip satisfiedAudio = null;
            AudioClip dissatisfiedAudio = null;

            if (satisfiedAudienceMembers >= 10)
            {
                satisfiedAudio = getRandomAudioFromList(_bigGoodResponses);
            }
            else if (satisfiedAudienceMembers >= 8)
            {
                satisfiedAudio = getRandomAudioFromList(_moderateGoodResponses);
            }
            else if (satisfiedAudienceMembers >= 4)
            {
                satisfiedAudio = getRandomAudioFromList(_smallGoodResponses);
            }
            else if (satisfiedAudienceMembers >= 1)
            {
                satisfiedAudio = getRandomAudioFromList(_tinyGoodResponses);
            }

            if (dissatisfiedAudienceMembers >= 10)
            {
                    dissatisfiedAudio = getRandomAudioFromList(_bigBadResponses);
            }
            else if (dissatisfiedAudienceMembers >= 8)
            {
                    dissatisfiedAudio = getRandomAudioFromList(_moderateBadResponses);
            }
            else if (dissatisfiedAudienceMembers >= 4)
            {
                    dissatisfiedAudio = getRandomAudioFromList(_smallBadResponses);
            }
            else if (dissatisfiedAudienceMembers >= 1)
            {
                    dissatisfiedAudio = getRandomAudioFromList(_tinyBadResponses);
            }

            _delayLenght = 0.0f;

            if (dissatisfiedAudio == null && satisfiedAudio == null)
            {
                AudioManager.Instance.PlaySFX(neutralAudio);
                _delayLenght = neutralAudio.length;
            }
            else
            {
                if (satisfiedAudio != null)
                {
                    AudioManager.Instance.PlaySFX(satisfiedAudio);
                    if (satisfiedAudio.length >= _delayLenght)
                    {
                        _delayLenght = satisfiedAudio.length;
                    }
                }

                if (dissatisfiedAudio != null)
                {
                    AudioManager.Instance.PlaySFX(dissatisfiedAudio);
                    if (dissatisfiedAudio.length >= _delayLenght)
                    {
                        _delayLenght = dissatisfiedAudio.length;
                    }
                }

            }
        }

        private AudioClip getRandomAudioFromList(AudioClip[] audioList)
        {
            if (audioList.Length == 0) return null;
            return audioList[Random.Range(0, audioList.Length)];
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