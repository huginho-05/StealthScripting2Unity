using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    [SerializeField] private EnemyHealth enemy;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Image fillImage; 

    void Start()
    {
        if (enemy != null && healthSlider != null)
        {
            healthSlider.minValue = 0;
            healthSlider.maxValue = enemy.GetMaxLife();
            healthSlider.value = enemy.GetCurrentLife();
        }
    }

    void Update()
    {
        if (enemy == null || healthSlider == null) return;
        
        float currentHealth = Mathf.Clamp(enemy.GetCurrentLife(), 0, enemy.GetMaxLife());
        healthSlider.value = currentHealth;
        
        if (fillImage != null)
            fillImage.enabled = currentHealth > 0.01f;
    }
}
