using UnityEngine;

public class DisappearingWall : MonoBehaviour
{
    public GameObject wall;
    [SerializeField] private SpriteRenderer rend;
    private SpriteRenderer[] _childRends;
    [SerializeField] private BoxCollider2D boxCol;
    [SerializeField] private int buttonsRemaining;


    void Awake()
    {
        if (boxCol == null)
        {
            boxCol = wall.GetComponent<BoxCollider2D>() as BoxCollider2D;
        }

        if (transform.childCount > 0)
        {
            _childRends = new SpriteRenderer[transform.childCount];
            for (int i = 0; i<transform.childCount; i++)
            {
                _childRends[i] = transform.GetChild(i).GetComponent<SpriteRenderer>() as SpriteRenderer;
            }
        }

        if (rend == null)
        {
            rend = wall.GetComponent<SpriteRenderer>() as SpriteRenderer;
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
        if (_childRends != null)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                _childRends[i].enabled = false;
            } 
        }
        rend.enabled = false;
        boxCol.enabled = false;
    }
}