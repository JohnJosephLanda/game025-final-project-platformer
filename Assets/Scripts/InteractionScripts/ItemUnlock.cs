using UnityEngine;
using System;

public class ItemUnlock : MonoBehaviour, IUsesInputManager
{
    // private bool canInteract;
    [SerializeField] private string ItemName;
    private IInputManager _InputManager;
    [SerializeField] private GameObject player;
    [SerializeField] private BasicPlayerMovement playerScript;

    void Start()
    {
        if (ItemName == null)
        {
            throw new ArgumentException("Item Name does not have a value");
        }
        
        if (player == null)
        {
            throw new ArgumentException("Player does not have a value");
        }
        
        if (BasicPlayerMovement == null)
        {
            try 
            {
                playerScript = player.GetComponent<BasicPlayerMovement>() as BasicPlayerMovement;
            }
            catch (Exception e)
            {
                throw new ArgumentException("Player Script does not have a value and cannot default to the player script");
            }
        }
    }
    
    public void SetInputManagerDependency(IInputManager InputManager)
    {
        _InputManager = InputManager;
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

    public void OnInteraction()
    {
        // likely will trigger something in the player to unlock an ability
        throw new NotImplementedException("Item Unlock does not yet have the OnInteraction() function written.");
    }

    public string GetItemName()
    {
        return ItemName;
    }
}