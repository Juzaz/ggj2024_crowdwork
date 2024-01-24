using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GlobalGameJam;

public class ChangeSceneButton : MonoBehaviour
{
    public SceneEnums SceneToLoad = SceneEnums.Menu;

    public void Button_ChangeScene()
    {
        ApplicationCore.Instance.ChangeScene(SceneToLoad);
    }
}