using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseState : BaseGameState
{
    private GameObject _pauseMenu;
    public PauseState(GameObject pauseMenu, GameManager parent) : base(parent)
    {
        _pauseMenu = pauseMenu;
    }

    public override void EndState()
    {
        base.EndState();
        Time.timeScale = 1.0f;
        _pauseMenu.SetActive(false);
        InputManager.Instance.onPressJump -= Resume;
    }

    public override void EnterState()
    {
        base.EnterState();
        Time.timeScale = 0.0f;
        _pauseMenu.SetActive(true);
        InputManager.Instance.onPressJump += Resume;
    }

    private void Resume()
    {
        parent.SetState(parent.runState);
    }
}
