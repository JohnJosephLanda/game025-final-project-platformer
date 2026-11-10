using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject target;
    public float followSpeed;
    private float toPlayer = 0;

    // Update is called once per frame
    void Update()
    {
         toPlayer = Vector2.Distance(transform.position, target.transform.position);
        if (toPlayer > 6)
            followSpeed = 7;
        else 
            followSpeed = 5;
        transform.position = Vector2.MoveTowards(transform.position , target.transform.position , followSpeed * Time.deltaTime);
        transform.position = new Vector3(transform.position.x, transform.position.y, -10);
    }
}
