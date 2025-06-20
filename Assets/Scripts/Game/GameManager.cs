using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject player;
    public Transform spawnPoint;
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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }
    public void HandlePlayerDead()
    {
        // Handle player death logic here
    }
}
