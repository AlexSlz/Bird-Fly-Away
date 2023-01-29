using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseGameState
{
    protected GameManager parent;
    public BaseGameState(GameManager parent)
    {
        this.parent = parent;
    }

    public virtual bool isActive { get; protected set; }

    public virtual void EnterState()
    {
        isActive = true;
    }
    public virtual void EndState()
    {
        isActive = false;
    }
}
