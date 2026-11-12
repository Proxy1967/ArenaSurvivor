using UnityEngine;
using System.Collections;

public class DamageBlink : MonoBehaviour
{
    private Health health;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private float blinkInterval = 0.1f;

    void Awake()
    {
        health = GetComponent<Health>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    
    void Start()
    {
        health.OnHealthChanged += HandleBlink;
    }

    void HandleBlink(int current, int max)
    {
        // if dead skip blink - death freezes time (timeScale = 0)
        // blinking would cause the player to disappear in the death screen.
        // Assumes a blink never outlasts the invincibility
        if (current <= 0) return;
        
        StartCoroutine(Blink());
    }

    IEnumerator Blink()
    {
        float elapsedTime = 0f;
        
        while(elapsedTime < health.GetInvincibleTime())
        {
            spriteRenderer.enabled = !spriteRenderer.enabled;
            yield return new WaitForSeconds(blinkInterval);
            elapsedTime += blinkInterval;
        }

        spriteRenderer.enabled = true;
    }
}
