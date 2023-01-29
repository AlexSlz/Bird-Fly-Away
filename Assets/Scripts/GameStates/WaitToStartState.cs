using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitToStartState : BaseGameState
{
    public WaitToStartState(GameManager parent) : base(parent)
    {
    }

    public override void EndState()
    {
        base.EndState();
        Time.timeScale = 1.0f;
        InputManager.Instance.onPressJump -= Start;
    }

    public override void EnterState()
    {
        base.EnterState();
        InputManager.Instance.onPressJump += Start;
        Time.timeScale = 0.0f;
    }

    private void Start()
    {
        parent.Player.Move();
        parent.SetState(parent.runState);
    }

}
