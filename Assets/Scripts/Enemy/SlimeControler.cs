using UnityEngine;

public class SlimeControler : MonoBehaviour
{
    private bool isInLight;
    public float lightCheckRaduis;
    public LayerMask whatIsLight;
    public Transform lightCheck;
    public GameObject visual;
    private Animator blobAnimator;
    public BasicMovement myMove;
    public GameObject[] eyes;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        blobAnimator = visual.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        isInLight = Physics2D.OverlapCircle(lightCheck.position, lightCheckRaduis, whatIsLight);
        blobAnimator.SetBool("Light", isInLight);
        if (isInLight)
        {
            myMove.SetSpeed(2);
            for(int i = 0; i < 2; i++)
            {
                eyes[i].SetActive(false);
            }
        }
        else 
        {
            myMove.SetSpeed(8);
            for(int i = 0; i < 2; i++)
            {
                eyes[i].SetActive(true);
            }
        }
    }
}
