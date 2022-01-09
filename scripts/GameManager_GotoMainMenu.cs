using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager_GotoMainMenu : MonoBehaviour
{
    //hyödynnetty tutoriaalia: https://www.youtube.com/watch?v=FwAWMQ7Fi6k&feature=youtu.be

    private GameManager_Master gameManagerMaster;

    void OnEnable()
    {
        gameManagerMaster = GetComponent<GameManager_Master>();
        gameManagerMaster.GoToMenuSceneEvent += GotoMainMenuScene;
    }

    void OnDisable()
    {
        gameManagerMaster.GoToMenuSceneEvent -= GotoMainMenuScene;
    }

    void GotoMainMenuScene()
    {
        SceneManager.LoadScene(0);
    }
}
