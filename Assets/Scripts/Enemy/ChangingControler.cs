using UnityEngine;
using System.Collections;

public class ChangingControler : MonoBehaviour
{
    private bool isInLight;
    public float lightCheckRaduis;
    public LayerMask whatIsLight;
    public Transform lightCheck;
    public GameObject happyVisual;
    public GameObject angryVisual;
    public int moveSpeed;
    public float extraLightDuration;
    private bool extraLight = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        isInLight = Physics2D.OverlapCircle(lightCheck.position, lightCheckRaduis, whatIsLight);
        if (isInLight)
        {
            angryVisual.SetActive(true);
            happyVisual.SetActive(false);
        }
        else if (!isInLight && angryVisual.activeSelf && extraLight == false)
        {
            extraLight = true;
            StartCoroutine(bonusAngry());
        }
    }

    private IEnumerator bonusAngry()
    {
        yield return new WaitForSeconds(extraLightDuration);
        if (!isInLight)
        {
            angryVisual.SetActive(false);
            happyVisual.SetActive(true);
        }
        extraLight = false;
    }
}
