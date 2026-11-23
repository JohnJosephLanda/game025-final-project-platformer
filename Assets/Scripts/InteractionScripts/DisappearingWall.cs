using UnityEngine;

public class DisappearingWall : MonoBehaviour
{
    public GameObject wall;
    [SerializeField] private SpriteRenderer rend;
    [SerializeField] private BoxCollider2D boxCol;
    [SerializeField] private int buttonsRemaining;


    void Awake()
    {
        if (rend == null)
        {
            rend = wall.GetComponent<SpriteRenderer>() as SpriteRenderer;
        }
        
        if (boxCol == null)
        {
            boxCol = wall.GetComponent<BoxCollider2D>() as BoxCollider2D;
        }
    }


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
        // wall.SetActive(false); // would work but would make it harder to store later
        rend.enabled = false;
        boxCol.enabled = false;
    }
}