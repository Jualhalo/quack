using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_QuitGame : MonoBehaviour
{
    private GameManager_Master gameManagerMaster;

    void OnEnable()
    {
        gameManagerMaster = GetComponent<GameManager_Master>();
        gameManagerMaster.QuitGameEvent += QuitGame;
    }

    void OnDisable()
    {
        gameManagerMaster.QuitGameEvent -= QuitGame;
    }

    void QuitGame()
    {
        Application.Quit();
    }
}
