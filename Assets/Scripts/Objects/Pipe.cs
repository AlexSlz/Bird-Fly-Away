using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : ObjectBase
{
    public override void Collide(Player player)
    {
        var direction = player.gameObject.transform.position.x <= this.transform.position.x;
        player.RigidBody.velocity = new Vector3((direction) ? -3 : 3, 4, 0);
        player.Dead();
    }
}
