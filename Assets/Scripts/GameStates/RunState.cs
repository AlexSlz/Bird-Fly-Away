using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunState : BaseGameState
{
    public RunState(GameManager parent) : base(parent)
    {
    }

    public override void EndState()
    {
        base.EndState();
        InputManager.Instance.onPressJump -= parent.Player.Move;
    }

    public override void EnterState()
    {
        base.EnterState();
        InputManager.Instance.onPressJump += parent.Player.Move;
    }
}
