using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RunState : BaseGameState
{
    private TMP_Text _helpText;
    public RunState(TMP_Text helpText, GameManager parent) : base(parent)
    {
        _helpText = helpText;
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
        _helpText.enabled = false;
    }
}
