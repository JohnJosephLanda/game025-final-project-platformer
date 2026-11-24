using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public string sceneName;
    public int spawnLocation;
    public GameManager gameManager;

    void Start()
    {
        gameManager = FindObjectsByType<GameManager>(FindObjectsSortMode.None)[0]; // should only have one game manager at start
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collided with level switch: "+other);
        if (other.CompareTag("Player"))
        {
            LoadScene(sceneName, spawnLocation);
        }
    }

    public void LoadScene(string scene, int spawnLocation)
    {
        gameManager.ChangeSceneProcess(scene, spawnLocation);
    }
}
