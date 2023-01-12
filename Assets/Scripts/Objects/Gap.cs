using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gap : ObjectBase
{
    public override void Collide(Player player)
    {
        Score.Instance.Add(1);
    }
}
