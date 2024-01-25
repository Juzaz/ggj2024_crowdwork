using UnityEngine;

namespace GlobalGameJam.Data
{
    [CreateAssetMenu(fileName = "joke_", menuName = "Datafiles/New Joke")]
    public class JokeData : ScriptableObject, IInitializableData
    {
        public string Joke = "";

        // runtime
        public string[] SplittedJoke { get; private set; }
        public int IdeaCount { get; private set; }

        public void InitializeData()
        {
            SplittedJoke = Joke.Split('§');
            IdeaCount = SplittedJoke.Length - 1;
        }

        public string GetJoke(IdeaData[] ideas)
        {
            string completedJoke = string.Empty;
            for (int i = 0; i < SplittedJoke.Length - 1; i++) // last one is end
            {
                completedJoke += SplittedJoke[i] + ideas[i].IdeaTextSprite;
            }
            completedJoke += SplittedJoke[SplittedJoke.Length - 1];

            return completedJoke;
        }
    }
}