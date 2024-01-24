using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GlobalGameJam.Data
{
    public class DatabaseManager : IInitializable
    {
        public static DatabaseManager Instance { get; private set; }

        private List<JokeData> _jokeList = new List<JokeData>();
        public List<JokeData> Jokes => _jokeList;

        private List<IdeaData> _ideaList = new List<IdeaData>();
        public List<IdeaData> Ideas => _ideaList;

        private List<AttributeData> _attributeList = new List<AttributeData>();
        public List<AttributeData> Attributes => _attributeList;

        public IEnumerator Initialize()
        {
            Instance = this;

            _jokeList.AddRange(Resources.LoadAll<JokeData>("Datafiles/Jokes"));
            _ideaList.AddRange(Resources.LoadAll<IdeaData>("Datafiles/Ideas"));
            _attributeList.AddRange(Resources.LoadAll<AttributeData>("Datafiles/Attributes"));

            List<IInitializableData> initializableDatas = new List<IInitializableData>();
            initializableDatas.AddRange(_jokeList);
            initializableDatas.AddRange(_ideaList);
            initializableDatas.AddRange(_attributeList);

            for (int i = 0; i < initializableDatas.Count; i++)
            {
                initializableDatas[i].InitializeData();
            }

            yield break;
        }

        public List<JokeData> GetJokesWithIdeaCount(int ideaCount)
        {
            List<JokeData> jokeList = new List<JokeData>();
            for (int i = 0; i < _jokeList.Count; i++)
            {
                if (jokeList[i].IdeaCount == ideaCount)
                {
                    jokeList.Add(_jokeList[i]);
                }
            }

            return jokeList;
        }
    }
}