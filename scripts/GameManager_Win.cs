using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_Win : MonoBehaviour
{
    private GameManager_Master gameManagerMaster;
    public GameObject winGamePanel;
    public GameObject timer;

    void OnEnable()
    {
        gameManagerMaster = GetComponent<GameManager_Master>();
        gameManagerMaster.GameWonEvent += ShowYouWinMessage;
    }

    void OnDisable()
    {
        gameManagerMaster.GameWonEvent -= ShowYouWinMessage;
    }

    void ShowYouWinMessage()
    {
        if (winGamePanel != null)
        {
            winGamePanel.SetActive(true);
        }
        if (timer != null)
        {
            timer.SetActive(true);
        }
    }
}
