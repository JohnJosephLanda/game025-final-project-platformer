using UnityEngine;

public class SlimeControler : MonoBehaviour
{
    private bool isInLight;
    public float lightCheckRaduis;
    public LayerMask whatIsLight;
    public Transform lightCheck;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        isInLight = Physics2D.OverlapCircle(lightCheck.position, lightCheckRaduis, whatIsLight);
        if (isInLight)
            print("THE LIGHT");
    }
}
