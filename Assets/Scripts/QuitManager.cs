using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitManager : MonoBehaviour
{
    [SerializeField] private GameObject _quitButton = null;

#if UNITY_WEBGL
    private void Awake()
    {
        _quitButton.SetActive(false);
    }
#endif

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            QuitGame();
        }
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}