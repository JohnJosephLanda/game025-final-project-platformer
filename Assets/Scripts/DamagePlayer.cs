using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    private BasicPlayerMovement player;

    public int damage = 10;
    public float knockbackAmount;
    public float knockbackDuration;
    public int direction;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<BasicPlayerMovement>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player") && player.iFrames <= 0) 
        {
            if (other.transform.position.x < transform.position.x)
                direction = -1;
            else
                direction = 1;
            player.startKnockback(knockbackDuration, knockbackAmount, direction);
            player.TakeDamage(damage);
        }
    }
}
