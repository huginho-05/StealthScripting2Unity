using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [Header("Player Health")]
    [SerializeField] private int playerMaxLife;
    [SerializeField] private int playerCurrentLife;
    

    void Start()
    {
        playerCurrentLife = playerMaxLife;
    }
    
    public void ReceiveDamage(int damage)
    {
        int damageTaken = Mathf.Max(damage, 1);
        playerCurrentLife -= damageTaken;
        if (playerCurrentLife <= 0)
        {
            SceneManager.LoadSceneAsync(1);
        }
    }
    
    public void AddHealth(int amount)
    {
        playerCurrentLife += amount;
        playerCurrentLife = Mathf.Clamp(playerCurrentLife, 0, playerMaxLife);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
            ReceiveDamage(10); 
        }
        if (collision.gameObject.CompareTag("DeathTrigger"))
        {
            ReceiveDamage(100); 
        }
    }
    
    public int GetCurrentLife()
    {
        return playerCurrentLife;
    }

    public int GetMaxLife()
    {
        return playerMaxLife;
    }
  
}

