using UnityEngine;

public class DisappearingWall : MonoBehaviour
{
    public GameObject wall;
    [SerializeField] private int buttonsRemaining;


    public void ButtonClickedToRemoveWall()
    {
        buttonsRemaining = buttonsRemaining - 1;

        if (CanRemoveWall())
        {
            RemoveWall();
        }
    }

    private bool CanRemoveWall()
    {
        return buttonsRemaining <= 0;
    }

    private void RemoveWall()
    {
        // may eventually want a cool shaking animation and such here later
        wall.SetActive(false);
    }
}