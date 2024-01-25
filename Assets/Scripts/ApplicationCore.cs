using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using GlobalGameJam.Data;

namespace GlobalGameJam
{
    public class ApplicationCore : MonoBehaviour
    {
        public static bool IsInitialized { get; private set; } = false;
        public static ApplicationCore Instance { get; private set; } = null;

        private SceneEnums _currentScene = SceneEnums.Menu;

        private void Awake()
        {
            Instance = this;
            GameObject.DontDestroyOnLoad(this);

            StartCoroutine(InitializeApplication());
        }

        private IEnumerator InitializeApplication()
        {
            List<IInitializable> initializations = new List<IInitializable>();
            initializations.Add(new DatabaseManager());

            initializations.AddRange(GetComponentsInChildren<IInitializable>());

            for (int i = 0; i < initializations.Count; i++)
            {
                IEnumerator initialization = initializations[i].Initialize();
                while (initialization.MoveNext())
                {
                    yield return initialization.Current;
                }
            }

            IsInitialized = true;
            _currentScene = (SceneEnums)SceneManager.GetActiveScene().buildIndex;

            yield break;
        }

        public void ChangeScene(SceneEnums scene)
        {
            if (_currentScene == scene)
            {
                Debug.LogError($"Already loaded in scene: {scene}");
                return;
            }

            Debug.Log($"Changing to scene: {scene}");
            _currentScene = scene;

            SceneManager.LoadScene((int)scene);
        }
    }
}