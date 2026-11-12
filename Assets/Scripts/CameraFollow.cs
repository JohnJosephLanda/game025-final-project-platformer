using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject target;
    public float followSpeed;
    private float toPlayer = 0;

    // Update is called once per frame
    void FixedUpdate()
    {
        toPlayer = Vector2.Distance(transform.position, target.transform.position);
        followSpeed = Vector3.Magnitude(target.GetComponent<Rigidbody2D>().linearVelocity);
        if (toPlayer > 5)
            followSpeed *= 1;
        else 
            followSpeed *= 0.8f;
        transform.position = Vector2.MoveTowards(transform.position , target.transform.position , followSpeed * Time.deltaTime);
        transform.position = new Vector3(transform.position.x, transform.position.y, -10);
    }
}
