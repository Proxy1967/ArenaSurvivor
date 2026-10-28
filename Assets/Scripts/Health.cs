using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    private int currentHealth;
    private bool isDead = false;


    void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        if(isDead)
        {
            return;
        }

        if (amount < 0)
        {
            Debug.LogWarning("Amount must not be negative");
            return;
        }

        currentHealth -= amount;
        currentHealth = Mathf.Max(0, currentHealth);
        
        if (currentHealth <= 0)
        {
            if(gameObject.CompareTag("Enemy")) {
                Debug.Log("Enemy dead");
                Destroy(gameObject);
            } 
            else {
                Debug.Log("You died!");
            }

            isDead = true;

        }
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    [ContextMenu("Take 10 Damage")]
    public void DamageTen()
    {
        TakeDamage(10);
    }

    [ContextMenu("Take -5 Damage")]
    public void DamageMinusFive()
    {
        TakeDamage(-5);
    }
}
