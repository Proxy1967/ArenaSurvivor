using UnityEngine;

public class EnemyChase : MonoBehaviour
{
    [SerializeField] private Rigidbody2D player;
    private Rigidbody2D rb;
    [SerializeField] private float moveSpeed = 3f;

    
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void FixedUpdate()
    {
        Vector2 normalizedDirection = (player.position - rb.position).normalized;
        rb.linearVelocity = normalizedDirection * moveSpeed;
    }
}
