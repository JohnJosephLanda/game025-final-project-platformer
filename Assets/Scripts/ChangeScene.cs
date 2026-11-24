using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour, IUsesGameManager
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

    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    // I DO NOT RECOMMEND USING THIS
    public void LoadScene(int sceneNumber)
    {
        SceneManager.LoadScene(sceneNumber);
    }
}
