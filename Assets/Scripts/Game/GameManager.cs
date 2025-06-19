using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public GameObject player;
    public Transform spawnPoint;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       // Spawn player prefab at spawn point
       if (player != null && spawnPoint != null)
       {
           Instantiate(player, spawnPoint.position, spawnPoint.rotation);
       }
       else
       {
           Debug.LogError("Player prefab or spawn point is not assigned in the GameManager.");
       }
    }
    public void HandlePlayerDead()
    {
        // Handle player death logic here
    }
}
