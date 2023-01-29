using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gap : ObjectBase
{
    public override void Collide(Player player)
    {
        if(GameManager.Instance.runState.isActive)
            Score.Instance.Add(1);
    }
}
