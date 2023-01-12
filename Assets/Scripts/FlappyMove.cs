using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FlappyMove
{
    [SerializeField]
    private int _jumpHeight = 3;

    public void Move(Rigidbody2D rigidbody)
    {
        rigidbody.velocity = Vector2.up * _jumpHeight;
    }
}
