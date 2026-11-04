using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    [SerializeField] private Health playerHealth;
    private Image image;

    void Awake()
    {
        image = GetComponent<Image>();
    }

    void Start()
    {
        playerHealth.OnHealthChanged += HandleHealthChange;
        HandleHealthChange(playerHealth.GetCurrentHealth(), playerHealth.GetMaxHealth());
    }

    private void HandleHealthChange(int current, int max)
    {
        image.fillAmount = (float)current / max;
    }
}
