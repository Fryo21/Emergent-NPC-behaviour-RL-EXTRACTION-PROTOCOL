using Unity.MLAgents.Integrations.Match3;
using Unity.VisualScripting;
using UnityEngine;

public class BulletsProjectile : MonoBehaviour
{
    [SerializeField] private float bulletSpeed = 20f;
    [SerializeField] private float bulletDamage = 40f;
    [SerializeField] private float gunShotRadius = 100f;
    [SerializeField] private LayerMask gunShotAlert;
    
    // Get a reference to the Rigidbody component
    private Rigidbody bulletRigidBody;

    private void Awake()
    {
        bulletRigidBody = GetComponent<Rigidbody>();
    }
    void Start()
    {
        // bullet should only be able to travel a certain distance, depend on the type of gun
        if (bulletRigidBody == null)
        {
            Debug.LogError("Rigidbody component not found on the GameObject.");
        }
       
        bulletRigidBody.linearVelocity = transform.forward * bulletSpeed;


        // Gun shots to create a shockwave of sound for otherr AI TO HEAR
        Collider[] alertHits = Physics.OverlapSphere(this.transform.position, gunShotRadius, gunShotAlert);
        
        Debug.Log("Gunshot alert hits: " + alertHits.Length);
        
        foreach (Collider hit in alertHits)
        {
            // Check if the hit object has a CivillianAIController component
            CivillianAIController civillianAI = hit.GetComponent<CivillianAIController>();
           
            if (civillianAI != null)
            {
                // Notify the civillian AI about the gunshot
                civillianAI.OnGunShotHeard(transform.position);
            }
            // Check if the hit object has a EnemyAIController component
        }
    }

    // Check if the bullet collides with an object
    private void OnCollisionEnter(Collision collision)
    {
        // check if it collided with civillian or enemy AI
        if (collision.gameObject.CompareTag("Civillian"))
        {
            Debug.Log(collision.gameObject.name + " was hit by the bullet.");

            // Get the CivillianHealth component and apply damage
            //CivillianHealth civillianHealth = collision.gameObject.GetComponent<CivillianHealth>();
            //civillianHealth.TakeDamage(bulletDamage);
        }
       // else if (collision.gameObject.CompareTag("EnemyAI"))
        //{
       //     Debug.Log(collision.gameObject.name + " was hit by the bullet.");
       // }
        else if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log(collision.gameObject.name + " was hit by the bullet.");
            // Get the PlayerHealth component and apply damage
            //PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            //playerHealth.TakeDamage(bulletDamage);
        }
        else
        {
            Debug.Log(collision.gameObject.name + " was hit by the bullet.");
        }

        Destroy(gameObject); 
    }
    
}
