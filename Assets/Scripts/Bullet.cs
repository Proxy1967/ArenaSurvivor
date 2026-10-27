using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D rb;
    private float speed = 8f;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    public void Launch(Vector2 direction)
    {
        rb.linearVelocity = direction * speed;
        Destroy(gameObject, 3f);
    }

}
