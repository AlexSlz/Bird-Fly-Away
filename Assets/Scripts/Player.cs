using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(CircleCollider2D))]
public class Player : MonoBehaviour
{
    [SerializeField]
    private FlappyMove _movement = new FlappyMove();
    public void Move() => _movement.Move(_rigidbody);

    private Rigidbody2D _rigidbody;
    public Rigidbody2D RigidBody => _rigidbody;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        this.GetComponent<SpriteRenderer>().color = GameAssets.Instance.ColorPalette.PlayerColor;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<ObjectBase>(out var myObject))
        {
            myObject.Collide(this);
        }
    }
}
