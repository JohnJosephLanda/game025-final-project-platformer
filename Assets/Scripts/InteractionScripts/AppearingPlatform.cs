using UnityEngine;

public class AppearingPlatform : MonoBehaviour
{
    public GameObject platform;
    [SerializeField] private int buttonsRemaining;


    public void ButtonClickedToAddPlatform()
    {
        buttonsRemaining = buttonsRemaining - 1;

        if (CanAddPlatform())
        {
            AddPlatform();
        }
    }

    private bool CanAddPlatform()
    {
        return buttonsRemaining <= 0;
    }

    private void AddPlatform()
    {
        // may eventually want a cool shaking animation and such here later
        platform.SetActive(true);
    }
}