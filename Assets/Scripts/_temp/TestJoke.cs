using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using GlobalGameJam.Data;

public class TestJoke : MonoBehaviour
{
    [SerializeField] TMP_Text _jokeContentText = null;
    [SerializeField] JokeData _joke = null;
    [SerializeField] IdeaData[] _ideas = null;

    void testLoad()
    {
        _joke.InitializeData();
        for (int i = 0; i < _ideas.Length; i++)
        {
            _ideas[i].InitializeData();
        }

        _jokeContentText.SetText(_joke.GetJoke(_ideas));
    }
}
