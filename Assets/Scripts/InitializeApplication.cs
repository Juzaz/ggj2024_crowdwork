using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GlobalGameJam
{
    public class InitializeApplication : MonoBehaviour
    {
        [SerializeField] private GameObject _applicationCorePrefab = null;

        private void Awake()
        {
            if (!ApplicationCore.IsInitialized)
            {
                StartCoroutine(coroutine_initializeApplication());
                return;
            }

            gameObject.SetActive(false);
        }

        private IEnumerator coroutine_initializeApplication()
        {
            GameObject applicationCore = GameObject.Instantiate(_applicationCorePrefab);
            while (!ApplicationCore.IsInitialized)
            {
                yield return null;
            }

            gameObject.SetActive(false);
            yield break;
        }
    }
}