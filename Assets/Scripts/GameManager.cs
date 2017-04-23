using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    const int MAIN_MENU_SCENE_INDEX = 1;

    private void Start()
    {
        LoadScene(MAIN_MENU_SCENE_INDEX);
    }

    public bool IsInMenu()
    {
        return SceneManager.sceneCount < 2 || (SceneManager.GetActiveScene() == SceneManager.GetSceneAt(MAIN_MENU_SCENE_INDEX));
    }

    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }
}
