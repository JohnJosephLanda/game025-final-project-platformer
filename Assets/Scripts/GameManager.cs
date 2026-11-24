using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private string _CurrentScene;
    private string _PrevScene;
    private int _RespawnLocationInScene; // starts at 0

    private bool _DoubleJumpUnlocked = false;
    private bool _DashUnlocked = false;
    private int _Sanity = 100;
    private float _FlashlightInnerAngle = 10f;
    private float _FlashlightOuterAngle = 10f;
    
    // private GameObject playerObject;
    private BasicPlayerMovement playerScript;





    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("gameManager");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);

        Debug.Log("here2");

        EnterSceneChecklist("Mansion1", 0);
    }








    public void ChangeSceneProcess(string sceneName, int spawnLocation) {
        UpdatePlayerStatus();
        SceneManager.LoadScene(sceneName);
        EnterSceneChecklist(sceneName, spawnLocation);
    }

    public void EnterSceneChecklist(string currentScene, int respawnLocation)
    {
        // SetDependencies(); // most scripts can do this themselves
        SceneRespawnSetup(currentScene, respawnLocation);
        GetPlayerAttributes();
        SetPlayerAttributes();
        Debug.Log("here");
    }







    private void UpdatePlayerStatus()
    {
        _FlashlightInnerAngle = playerScript.GetFlashlightInnerAngle();
        _FlashlightOuterAngle = playerScript.GetFlashlightOuterAngle();
        _DoubleJumpUnlocked = playerScript.GetDoubleJumpUnlocked();
        _DashUnlocked = playerScript.GetDashUnlocked();
        _Sanity = playerScript.GetSanity();
    }


    private void SceneRespawnSetup(string currentScene, int respawnLocation)
    {
        _PrevScene = _CurrentScene;
        _CurrentScene = currentScene;
        _RespawnLocationInScene = respawnLocation;
    }



    private void GetPlayerAttributes()
    {
        playerScript = FindObjectsByType<BasicPlayerMovement>(FindObjectsSortMode.None)[0]; // assumes there's only one player
    }



    private void SetPlayerAttributes()
    {
        playerScript.SetFlashlightSize(_FlashlightInnerAngle, _FlashlightOuterAngle);
        playerScript.SetAbilities(_DoubleJumpUnlocked, _DashUnlocked);
        playerScript.SetSanity(_Sanity);
    }
}