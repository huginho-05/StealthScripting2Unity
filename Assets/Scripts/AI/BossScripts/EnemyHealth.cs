using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("Enemy Health")]
    [SerializeField] private int enemyMaxLife;
    [SerializeField] private int enemyCurrentLife;
    

    void Start()
    {
        enemyCurrentLife = enemyMaxLife;
    }
    
    public void ReceiveDamage(int damage)
    {
        int damageTaken = Mathf.Max(damage, 1);
        enemyCurrentLife -= damageTaken;
        if (enemyCurrentLife <= 0)
        {
            //Poner que muere
        }
    }
    
    public void AddHealth(int amount)
    {
        enemyCurrentLife += amount;
        enemyCurrentLife = Mathf.Clamp(enemyCurrentLife, 0, enemyMaxLife);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ReceiveDamage(10); 
        }
    }
    
    public int GetCurrentLife()
    {
        return enemyCurrentLife;
    }

    public int GetMaxLife()
    {
        return enemyMaxLife;
    }
}
