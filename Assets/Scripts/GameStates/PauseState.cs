using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class PauseState : BaseGameState
{
    [SerializeField]
    private int _waitTime = 3;

    private TMP_Text _timerView;
    private GameObject _pauseMenu;

    public PauseState(GameObject pauseMenu, TMP_Text timerView, GameManager parent) : base(parent)
    {
        _pauseMenu = pauseMenu;
        _timerView = timerView;
    }

    public override void EndState()
    {
        base.EndState();
        parent.StartCoroutine(WaitingBeforeResume());
    }

    public override void EnterState()
    {
        base.EnterState();
        Time.timeScale = 0.0f;
        _pauseMenu.SetActive(true);
        InputManager.Instance.onPressJump += Resume;
    }

    private IEnumerator WaitingBeforeResume()
    {
        _pauseMenu.SetActive(false);
        for (int i = _waitTime; i > 0; i--)
        {
            _timerView.text = $"{i}";
            yield return new WaitForSecondsRealtime(.5f);
        }
        _timerView.text = "";

        Time.timeScale = 1f;
        InputManager.Instance.onPressJump -= Resume;
        yield return null;
    }


    private void Resume()
    {
        parent.SetState(parent.runState);
    }
}
