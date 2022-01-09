using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_ToggleCursor : MonoBehaviour
{
    //hyödynnetty tutoriaalia: https://www.youtube.com/watch?v=L-xG2JxW-hY
    private GameManager_Master gameManagerMaster;
    private bool cursorLocked = true;

    void OnEnable()
    {
        gameManagerMaster = GetComponent<GameManager_Master>();
        gameManagerMaster.MenuToggleEvent += ToggleCursorState;
    }

    void OnDisable()
    {
        gameManagerMaster.MenuToggleEvent -= ToggleCursorState;
    }

    void Update()
    {
        CheckCursorLock();
    }

    void ToggleCursorState()
    {
        cursorLocked = !cursorLocked;
    }

    void CheckCursorLock()
    {
        if (cursorLocked)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
