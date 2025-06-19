using Unity.MLAgents.Integrations.Match3;
using UnityEngine;

public class BulletsProjectile : MonoBehaviour
{
    [SerializeField] private float bulletSpeed = 20f;
    // Get a reference to the Rigidbody component
    private Rigidbody bulletRigidBody;


    private void Awake()
    {
        bulletRigidBody = GetComponent<Rigidbody>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (bulletRigidBody == null)
        {
            Debug.LogError("Rigidbody component not found on the GameObject.");
        }
        Debug.Log("Spwaning position is " + transform.position + " with rotation " + transform.rotation);
        bulletRigidBody.velocity = transform.forward * bulletSpeed;
        
    }

    // Check if the bullet collides with an object
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name + " was hit by the bullet.");
        Destroy(gameObject); // Destroy the bullet on collision
    }
}
