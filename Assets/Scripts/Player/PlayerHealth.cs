using Unity.VisualScripting;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100;
    [SerializeField] private float currentHealth;

    [Header("Health Regeneration Settings")]
    [SerializeField] private float healthRestorePoint = 5f;
    [SerializeField] private float healthRestoreRate = 1f; 

    void Start()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        if (currentHealth < maxHealth && currentHealth > 0)
        {
            RestoreHealth();
        }
    }
    // Restore health over time if the player is not at full health
    private void RestoreHealth()
    {
        currentHealth += healthRestorePoint * Time.deltaTime * healthRestoreRate;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
    }
    // Method to take damage, reducing the player's health
    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;
        Debug.Log("Player took damage: " + damageAmount + ". Current health: " + currentHealth);
        
        if (currentHealth <= 0)
        {
            // if player is dead noify the game manager
            Debug.Log("Player is dead.");            
            GameManager.Instance.HandlePlayerDead();
        }
    }
    // Method to get the current and max health values
    public float GetCurrentHealth()
    {
        return currentHealth;
    }
    // Method to get the maximum health value
    public float GetMaxHealth()
    {
        return maxHealth;
    }
    // Method to increase the player's health, clamping it to the maximum health value

    public void IncreaseHealth(float amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);     
    }
}
