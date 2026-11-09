using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class FollowMouse : MonoBehaviour
{
    private float mouseX;
    private float mouseY;
    private float angle;
    private quaternion finalAngle;

    // Update is called once per frame
    void Update()
    {
        mouseX = Input.mousePosition.x;
        mouseY = Input.mousePosition.y;
        mouseX -= this.transform.position.x;
        mouseY += this.transform.position.y;
        angle = Mathf.Cos(mouseX/mouseY);
        finalAngle = quaternion.Euler(0, 0, angle);
        this.transform.rotation = finalAngle;
    }
}
