using UnityEngine;

public class CivillianHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth = 20;
    private float currentHealth;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    void Awake()
    {
        currentHealth = maxHealth; // Initialize current health to max health
    }
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        Debug.Log("Civillian has died.");
        gameObject.SetActive(false); 
        Destroy(this.gameObject, 2f);
    }

}
