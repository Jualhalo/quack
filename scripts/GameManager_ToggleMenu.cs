using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_ToggleMenu : MonoBehaviour
{
    //hyödynnetty tutoriaalia: https://www.youtube.com/watch?v=wVS9aGvrFwY
    private GameManager_Master gameManagerMaster;
    public GameObject menu;

    void OnEnable()
    {
        gameManagerMaster = GetComponent<GameManager_Master>();
        gameManagerMaster.GameOverEvent += ToggleMenu;
        gameManagerMaster.GameWonEvent += ToggleMenu;
    }

    void OnDisable()
    {
        gameManagerMaster.GameOverEvent -= ToggleMenu;
        gameManagerMaster.GameWonEvent -= ToggleMenu;
    }

    void Update()
    {
        CheckForMenuToggleRequest();
    }

    void CheckForMenuToggleRequest()
    {
        if (Input.GetKeyUp(KeyCode.Escape) && !gameManagerMaster.isGameOver && !gameManagerMaster.win)
        {
            ToggleMenu();
        }
    }

    void ToggleMenu()
    {
        if (menu != null)
        {
            menu.SetActive(!menu.activeSelf);
            gameManagerMaster.isMenuOn = !gameManagerMaster.isMenuOn;
            gameManagerMaster.CallEventMenuToggle();
        }
    }
}
