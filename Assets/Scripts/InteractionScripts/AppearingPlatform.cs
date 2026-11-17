using UnityEngine;

public class AppearingPlatform : MonoBehaviour
{
    public GameObject platform;
    [SerializeField] private SpriteRenderer rend;
    [SerializeField] private BoxCollider2D boxCol;
    [SerializeField] private int buttonsRemaining;


    void Start()
    {
        if (rend == null)
        {
            rend = platform.GetComponent<SpriteRenderer>();
        }
        
        if (boxCol == null)
        {
            boxCol = platform.GetComponent<BoxCollider2D>();
        }
    }


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
        // platform.SetActive(True); // Won't work because the script won't be enabled
        rend.enabled = true;
        boxCol.enabled = true;
    }
}