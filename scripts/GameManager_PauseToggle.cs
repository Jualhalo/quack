using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_PauseToggle : MonoBehaviour
{
    //hyödynnetty tutoriaalia: https://www.youtube.com/watch?v=PmX5UM0PyF8
    private GameManager_Master gameManagerMaster;
    private bool isPaused;

    void OnEnable()
    {
        Time.timeScale = 1;
        gameManagerMaster = GetComponent<GameManager_Master>();
        gameManagerMaster.MenuToggleEvent += TogglePause;
    }

    void OnDisable()
    {
        gameManagerMaster.MenuToggleEvent -= TogglePause;
    }

    void TogglePause()
    {
        if (isPaused)
        {
            Time.timeScale = 1;
            isPaused = false;
        }
        else
        {
            Time.timeScale = 0;
            isPaused = true;
        }
    }
}
