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
    public GameObject target;
    private Rigidbody2D myRigidBody;
    public float accerate;
    public float scale;

    private bool playerInRange;
    public float plyaerCheckRaduis;
    public LayerMask whatIsPlayer;
    public Transform playerCheck;
    private float attackOnCooldown;
    public float attackCooldownAmount;
    public Animator myAnim;
    public float attackSpeed;
    private bool inAttack = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
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

        if (attackOnCooldown > 0)
            attackOnCooldown -= Time.deltaTime;
        
        if (isInLight || extraLight)
        {
            playerInRange = Physics2D.OverlapCircle(playerCheck.position, plyaerCheckRaduis, whatIsPlayer);
            if (playerInRange && attackOnCooldown <= 0 && !inAttack)
            {
                inAttack = true;
                myAnim.SetBool("InAttack", true);
                StartCoroutine(attack());
            }


            if (target.transform.position.x >= transform.position.x && !inAttack)
            {
                if (myRigidBody.linearVelocityX < moveSpeed)
                    myRigidBody.linearVelocity = new Vector2((Mathf.Abs(myRigidBody.linearVelocityX) + accerate), myRigidBody.linearVelocity.y);  
                else 
                    myRigidBody.linearVelocity = new Vector2(moveSpeed, myRigidBody.linearVelocity.y);
                transform.localScale = new Vector2(-scale, scale);
            }
            else if (target.transform.position.x <= transform.position.x && !inAttack)
            {
                if (myRigidBody.linearVelocityX > -moveSpeed)
                    myRigidBody.linearVelocity = new Vector2((-Mathf.Abs(myRigidBody.linearVelocityX) - accerate), myRigidBody.linearVelocity.y);  
                else 
                    myRigidBody.linearVelocity = new Vector2(-moveSpeed, myRigidBody.linearVelocity.y);
                transform.localScale = new Vector2(scale, scale);
            }
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
        myRigidBody.linearVelocity = new Vector2(0, myRigidBody.linearVelocity.y);
    }

    private IEnumerator attack()
    {
        if (target.transform.position.x >= transform.position.x)
        {
            myRigidBody.linearVelocity = new Vector2(attackSpeed, (3f + (target.transform.position.y - this.transform.position.y)));
            transform.localScale = new Vector2(-scale, scale);
        }
        else
        {
            myRigidBody.linearVelocity = new Vector2(-attackSpeed, (3f + (target.transform.position.y - this.transform.position.y)));
            transform.localScale = new Vector2(scale, scale);
        }

        yield return new WaitForSeconds(1.5f);
        myAnim.SetBool("FinalAttack", true);
        yield return new WaitForSeconds(0.3f);
        inAttack = false;
        myAnim.SetBool("FinalAttack", false);
        myAnim.SetBool("InAttack", false);
        attackOnCooldown = attackCooldownAmount;

    }
}
