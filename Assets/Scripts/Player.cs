using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(CircleCollider2D))]
public class Player : MonoBehaviour
{
    [SerializeField]
    private FlappyMove _movement = new FlappyMove();

    private Rigidbody2D _rigidbody;
    public Rigidbody2D RigidBody => _rigidbody;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            _movement.Move(_rigidbody);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<ObjectBase>(out var myObject))
        {
            myObject.Collide(this);
        }
    }

    public void Dead()
    {
        GameManager.Instance.Pause();
    }

}
