using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameManager_Master : MonoBehaviour
{
    //hyödynnetty tutoriaalia: https://www.youtube.com/watch?v=Pd80uJEfdE4

    public delegate void GameManagerEventHandler();

    public event GameManagerEventHandler MenuToggleEvent;
    public event GameManagerEventHandler RestartLevelEvent;
    public event GameManagerEventHandler QuitGameEvent;
    public event GameManagerEventHandler GoToMenuSceneEvent;
    public event GameManagerEventHandler GameOverEvent;
    public event GameManagerEventHandler GameWonEvent;

    public bool isGameOver;
    public bool isMenuOn;
    public bool win;

    public void CallEventMenuToggle()
    {
        if (MenuToggleEvent != null)
        {
            MenuToggleEvent();
        }
    }

    public void CallEventRestartLevel()
    {
        if (RestartLevelEvent != null)
        {
            RestartLevelEvent();
        }
    }

    public void CallEventQuitGame()
    {
        if (QuitGameEvent != null)
        {
            QuitGameEvent();
        }
    }

    public void CallEventGoToMenuScene()
    {
        if (GoToMenuSceneEvent != null)
        {
            GoToMenuSceneEvent();
        }
    }

    public void CallEventGameOverEvent()
    {
        if (GameOverEvent != null)
        {
            isGameOver = true;
            GameOverEvent();
        }
    }

    public void CallEventGameWonEvent()
    {
        if (GameWonEvent != null)
        {
            win = true;
            GameWonEvent();
        }
    }
}
