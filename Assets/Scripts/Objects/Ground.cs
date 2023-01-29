using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : ObjectBase
{
    public override void Collide(Player player)
    {
        GameManager.Instance.EndGame();
    }
}
