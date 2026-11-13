using UnityEngine;
using System;
using System.Collections;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    private int currentHealth;
    private bool isDead = false;
    public event Action OnDied;
    public event Action<int, int> OnHealthChanged; // current, max
    private bool isInvincible;
    [SerializeField] private float invincibleTime = 0f;


    void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        if(isDead || isInvincible)
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
        StartCoroutine(Invincible());
        OnHealthChanged?.Invoke(currentHealth, maxHealth);
        
        if (currentHealth <= 0)
        {
            OnDied?.Invoke();
            isDead = true;
        }
    }

    IEnumerator Invincible()
    {
        isInvincible = true;
        yield return new WaitForSeconds(invincibleTime);
        isInvincible = false;
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    public float GetInvincibleTime()
    {
        return invincibleTime;
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
