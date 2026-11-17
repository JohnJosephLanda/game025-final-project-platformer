using UnityEngine;
using UnityEngine.InputSystem;
using System;
using System.Linq;
using System.Collections.Generic;

public class InputManager : MonoBehaviour, IInputManager
{
    private List<IUsesInputManager> _InteractableObjects;

    void Awake()
    {
        _InteractableObjects = new List<IUsesInputManager>();

        var objectsNeedingInputManager = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None).OfType<IUsesInputManager>();

        foreach (IUsesInputManager obj in objectsNeedingInputManager)
        {
            obj.SetInputManagerDependency((IInputManager) (this));
            Debug.Log("Object that required InputManager: "+obj);
        }
    }

    public void SetObjectInteractable(IUsesInputManager obj)
    {
        _InteractableObjects.Add(obj);
    }

    public void SetObjectNonInteractable(IUsesInputManager obj)
    {
        if (!_InteractableObjects.Contains(obj)) {return;}
        _InteractableObjects.Remove(obj);
    }



    void Update()
    {
        if (Keyboard.current == null) return;
        
        if (_InteractableObjects.Count > 0 && Keyboard.current.eKey.wasPressedThisFrame)
        {
            foreach (IUsesInputManager obj in _InteractableObjects)
            {
                obj.OnInteraction();
            }
        }
    }
}