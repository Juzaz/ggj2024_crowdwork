using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

            StartCoroutine(coroutine_initializeApplication());
        }

        private IEnumerator coroutine_initializeApplication()
        {
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

            Debug.LogError($"Changing to scene: {scene}");
            _currentScene = scene;
            SceneManager.LoadScene((int)scene);
        }
    }
}