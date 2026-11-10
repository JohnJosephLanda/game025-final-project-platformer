using UnityEngine;
using System;

public class RemovingWallButton : MonoBehaviour
{
    [SerializeField] private GameObject linkedWall;
    private DisappearingWall disappearingLinkedWallScript;

    void Start()
    {
        disappearingLinkedWallScript = linkedWall.GetComponent(typeof(DisappearingWall)) as DisappearingWall;

        if (disappearingLinkedWallScript == null)
        {
            throw new ArgumentException("Linked wall does not have a DisappearingWall script");
        }
    }

    public void OnInteraction()
    {
        disappearingLinkedWallScript.ButtonClickedToRemoveWall();
    }
}