using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public bool IsDead;
    [SerializeField] private int maxHealth = 100;
    private int currentHealth;
    
    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        if (IsDead)
        {
            return;
        }
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            IsDead = true;
            GameManager.Instance.TriggerLose();
        }
    }
}