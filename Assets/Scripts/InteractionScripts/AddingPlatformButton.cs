using UnityEngine;
using System;

public class AddingPlatformButton : MonoBehaviour, IUsesInputManager
{
    [SerializeField] private GameObject linkedWall;
    private AppearingPlatform appearingLinkedPlatformScript;
    // private bool canInteract;
    private IInputManager _InputManager;

    void Start()
    {
        appearingLinkedPlatformScript = linkedWall.GetComponent(typeof(AppearingPlatform)) as AppearingPlatform;

        if (appearingLinkedPlatformScript == null)
        {
            throw new ArgumentException("Linked wall does not have a AppearingPlatform script");
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
        appearingLinkedPlatformScript.ButtonClickedToAddPlatform();
    }
}