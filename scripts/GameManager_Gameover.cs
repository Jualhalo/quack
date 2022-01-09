using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_Gameover : MonoBehaviour
{
    //hyödynnetty tutoriaalia: https://www.youtube.com/watch?v=9z7_tlX8OTg&feature=youtu.be
    private GameManager_Master gameManagerMaster;
    public GameObject gameOverPanel;

    void OnEnable()
    {
        gameManagerMaster = GetComponent<GameManager_Master>();
        gameManagerMaster.GameOverEvent += ShowYouDiedMessage;
    }

    void OnDisable()
    {
        gameManagerMaster.GameOverEvent -= ShowYouDiedMessage;
    }

    void ShowYouDiedMessage()
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }
    }
}
