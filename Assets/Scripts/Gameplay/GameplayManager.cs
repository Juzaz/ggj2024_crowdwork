using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GlobalGameJam.Gameplay
{
    public class GameplayManager : MonoBehaviour
    {
        [SerializeField] private GameObject _waitForInitialization = null;

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

            StartGameplay();
        }

        private void StartGameplay()
        {
            Debug.Log("Start gameplay");

            // Gameplay States:
            // Intro

            // Show Audience attributes
            // Show Ideas
            // Show force & angle
            // Shoot microphone
            // Collect Ideas

            // Get Joke with amount of ideas

            // Show Joke

            // Insert Ideas to the joke

            // Audience reaction

            // Point & health calculations

            // Check for end state

            // Jump to Show audio audience attributes
            
        }
    }
}