using UnityEngine;

public class DestroyOnDeath : MonoBehaviour
{

    private Health health;

    void Awake()
    {
        health = GetComponent<Health>();
        health.OnDied += HandleDeath;
    }

    public void HandleDeath()
    {
        Destroy(gameObject);
    }
}
