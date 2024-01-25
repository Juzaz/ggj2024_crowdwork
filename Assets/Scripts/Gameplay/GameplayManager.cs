using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GlobalGameJam.Data;
using GlobalGameJam.Gameplay.States;

namespace GlobalGameJam.Gameplay
{
    public class GameplayManager : MonoBehaviour
    {
        [Header("Initialization & Scene references")]

        [SerializeField] private GameObject _waitForInitialization = null;
        [SerializeField] private GameObject _audienceParent = null;
        [SerializeField] private GameObject _ideasParent = null;

        private List<AudienceMember> _audienceList = new List<AudienceMember>();
        private List<BaseState> _gameStateList = new List<BaseState>();
        private List<IdeaBubble> _ideaBubbles = new List<IdeaBubble>();

        private Dictionary<GameplayStateEnum, BaseState> _gameStateDictionary = new Dictionary<GameplayStateEnum, BaseState>();
        private GameplayStateEnum _gameplayState = GameplayStateEnum.Intro;

        public JokeData CurrentJoke { get; private set; }
        public IdeaData[] CurrentIdeas { get; private set; }

        private void Awake()
        {
            StartCoroutine(WaitForInitialization());
        }

        private IEnumerator WaitForInitialization()
        {
            while (_waitForInitialization.activeInHierarchy)
            {
                yield return null;
            }

            _gameStateList.AddRange(GetComponentsInChildren<BaseState>(true));
            _audienceList.AddRange(_audienceParent.GetComponentsInChildren<AudienceMember>());
            _ideaBubbles.AddRange(_ideasParent.GetComponentsInChildren<IdeaBubble>());

            List<IInitializable> initializationList = new List<IInitializable>();
            initializationList.AddRange(_gameStateList);
            initializationList.AddRange(_audienceList);
            initializationList.AddRange(_ideaBubbles);
            for (int i = 0; i < initializationList.Count; i++)
            {
                IEnumerator initialization = initializationList[i].Initialize();
                while (initialization.MoveNext())
                {
                    yield return initialization.Current;
                }
            }

            for (int i = 0; i < _gameStateList.Count; i++)
            {
                _gameStateList[i].SetReferences(this);
                _gameStateDictionary.Add(_gameStateList[i].State, _gameStateList[i]);
            }

            _gameplayState = GameplayStateEnum.Intro;
            _gameStateDictionary[_gameplayState].StartState(onGameStateFinished);
        }

        private void onGameStateFinished()
        {
            _gameStateDictionary[_gameplayState].EndState();

            if (_gameplayState == GameplayStateEnum.Cleanup) // Loop point
            {
                _gameplayState = GameplayStateEnum.AudienceAttributes;
            }
            else
            {
                _gameplayState = _gameplayState + 1;
            }

            _gameStateDictionary[_gameplayState].StartState(onGameStateFinished);
        }

        public void SetToldJoke(JokeData joke, IdeaData[] ideas)
        {
            CurrentJoke = joke;
            CurrentIdeas = ideas != null ? ideas : new IdeaData[0];
        }
    }
}