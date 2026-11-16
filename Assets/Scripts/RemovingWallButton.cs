using UnityEngine;
using System;

public class RemovingWallButton : MonoBehaviour, IUsesInputManager
{
    [SerializeField] private GameObject linkedWall;
    private DisappearingWall disappearingLinkedWallScript;
    // private bool canInteract;
    private IInputManager _InputManager;

    void Start()
    {
        disappearingLinkedWallScript = linkedWall.GetComponent(typeof(DisappearingWall)) as DisappearingWall;

        if (disappearingLinkedWallScript == null)
        {
            throw new ArgumentException("Linked wall does not have a DisappearingWall script");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collided with lever: "+other);
        if (other.CompareTag("Player"))
        {
            // canInteract = true;
            _InputManager.SetObjectInteractable((IUsesInputManager) this);
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("Collided with lever: "+other);
        if (other.CompareTag("Player"))
        {
            // canInteract = true;
            _InputManager.SetObjectNonInteractable((IUsesInputManager) this);
        }
    }

    public void SetInputManagerDependency(IInputManager InputManager)
    {
        _InputManager = InputManager;
    }

    public void OnInteraction()
    {
        disappearingLinkedWallScript.ButtonClickedToRemoveWall();
    }
}