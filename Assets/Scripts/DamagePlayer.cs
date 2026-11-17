using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    private BasicPlayerMovement player;

    public int damage = 10;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<BasicPlayerMovement>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player")) 
        {
            player.TakeDamage(damage);

            /*player.knockbackCount = player.knockbackDuration;

            if (other.transform.position.x < transform.position.x)
                player.knockFromRight = true;
            else
                player.knockFromRight = false;*/
        }
    }
}
