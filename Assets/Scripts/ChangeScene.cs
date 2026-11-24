using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public string sceneName;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collided with level switch: "+other);
        if (other.CompareTag("Player"))
        {
            LoadScene(sceneName);
        }
    }

    public void LoadScene(string scene, int spawnLocation)
    {
        GameManager.ChangeSceneProcess(scene, spawnLocation);
    }
}
