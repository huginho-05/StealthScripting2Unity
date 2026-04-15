using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    [SerializeField] private PlayerHealth player;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Image fillImage; 

    void Start()
    {
        if (player != null && healthSlider != null)
        {
            healthSlider.minValue = 0;
            healthSlider.maxValue = player.GetMaxLife();
            healthSlider.value = player.GetCurrentLife();
        }
    }

    void Update()
    {
        if (player == null || healthSlider == null) return;
        
        float currentHealth = Mathf.Clamp(player.GetCurrentLife(), 0, player.GetMaxLife());
        healthSlider.value = currentHealth;
        
        if (fillImage != null)
            fillImage.enabled = currentHealth > 0.01f;
    }
}
