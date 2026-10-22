using UnityEngine;

public class EnemyChase : MonoBehaviour
{
    [SerializeField] private Rigidbody2D player;
    private Rigidbody2D rb;
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private int damage = 5;
    [SerializeField] private int cooldown = 1;
    private float timeHit;

    
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void FixedUpdate()
    {
        Vector2 normalizedDirection = (player.position - rb.position).normalized;
        rb.linearVelocity = normalizedDirection * moveSpeed;
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            if(Time.time >= timeHit + cooldown)
            {
                collision.gameObject.GetComponent<Health>().TakeDamage(damage);
                timeHit = Time.time;
            }
        }
    }
}
