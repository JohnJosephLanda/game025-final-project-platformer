using UnityEngine;
using System;

public class ItemUnlock : MonoBehaviour, IUsesInputManager
{
    // private bool canInteract;
    [SerializeField] private ItemEnum ItemName;
    private IInputManager _InputManager;
    [SerializeField] private GameObject player;
    [SerializeField] private BasicPlayerMovement playerScript;
    private bool _ItemUnlockUsed = false;

    void Start()
    {
        if (ItemName == ItemEnum.noAssignedUpgrade)
        {
            throw new ArgumentException("Item Name does not have an assigned upgrade");
        }
        
        if (player == null)
        {
            throw new ArgumentException("Player does not have a value");
        }
        
        if (playerScript == null)
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
        Debug.Log("Collided with item unlock: "+other);
        if (!_ItemUnlockUsed && other.CompareTag("Player"))
        {
            // canInteract = true;
            _InputManager.SetObjectInteractable((IUsesInputManager) this);
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("Collided with item unlock: "+other);
        if (!_ItemUnlockUsed && other.CompareTag("Player"))
        {
            // canInteract = true;
            _InputManager.SetObjectNonInteractable((IUsesInputManager) this);
        }
    }




    public void OnInteraction()
    {
        _ItemUnlockUsed = playerScript.ItemUnlock(ItemName);
        
        if (_ItemUnlockUsed)
        {
            _InputManager.SetObjectNonInteractable((IUsesInputManager) this);
        }
    }




    public string GetItemName()
    {
        return ItemName.ToString();
    }
}