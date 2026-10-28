using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D rb;
    private float speed = 8f;
    [SerializeField] private int damage = 10;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    public void Launch(Vector2 direction)
    {
        rb.linearVelocity = direction * speed;
        Destroy(gameObject, 3f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            Health enemyHealth = other.GetComponent<Health>();
            enemyHealth.TakeDamage(damage);
            Destroy(gameObject);
        }
        
    }

}
