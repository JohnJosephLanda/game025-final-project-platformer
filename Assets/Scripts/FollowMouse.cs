using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class FollowMouse : MonoBehaviour
{
    private float mouseX;
    private float mouseY;
    private float angle;
    private quaternion finalAngle;
    private Vector3 mouseFull;

    // Update is called once per frame
    void Update()
    {
        /*mouseX = Input.mousePosition.x;
        mouseY = Input.mousePosition.y;
        mouseFull = new Vector3(mouseX, mouseY, 0);
        mouseFull = Camera.main.ScreenToWorldPoint(new Vector3(mouseX, mouseY, 0f));
        mouseX = mouseFull.x - this.transform.position.x;
        mouseY = mouseFull.y - this.transform.position.y;
        angle = Mathf.Atan2(mouseY, mouseX) * Mathf.Rad2Deg;
        finalAngle = quaternion.Euler(0, 0, angle);
        this.transform.rotation = finalAngle;*/

        Vector3 mousePos = Input.mousePosition;
		mousePos.z = 0;

		Vector3 objectPos = Camera.main.WorldToScreenPoint (transform.position);
		mousePos.x = mousePos.x - objectPos.x;
		mousePos.y = mousePos.y - objectPos.y;

		float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Euler(new Vector3(0, 0, (angle - 90)));
    }
}
