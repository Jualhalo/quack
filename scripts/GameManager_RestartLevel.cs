using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager_RestartLevel : MonoBehaviour
{
    //hyödynnetty tutoriaalia: https://www.youtube.com/watch?v=4YQV-46S_UI&feature=youtu.be

    private GameManager_Master gameManagerMaster;

    void OnEnable()
    {
        gameManagerMaster = GetComponent<GameManager_Master>();
        gameManagerMaster.RestartLevelEvent += RestartLevel;
    }

    void OnDisable()
    {
        gameManagerMaster.RestartLevelEvent -= RestartLevel;
    }

    void RestartLevel()
    {
        SceneManager.LoadScene(1);
    }
}
