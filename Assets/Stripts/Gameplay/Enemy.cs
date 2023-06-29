using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private float attackCooldown;
    [SerializeField] private float movingSpeed = 2f;
    [SerializeField] private GameObject[] wayPoints;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private float range;
    [SerializeField] private float colliderDistance;
    [SerializeField] private LayerMask playerlayer;
    [SerializeField] private float idleDuration;



    private float idleTime;
    private float cooldownTimer = Mathf.Infinity;
    private Animator anim;

    private int curWayPointIndex = 0;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        if (Vector2.Distance(this.transform.position, player.transform.forward) >= 3f)
        {
            if (Vector2.Distance(wayPoints[curWayPointIndex].transform.position, transform.position) < 0.1f)
            {
                flip();
                curWayPointIndex++;
                if (curWayPointIndex >= wayPoints.Length)
                { 
                    curWayPointIndex = 0;
                }

                transform.position = Vector2.MoveTowards(transform.position,
                    wayPoints[curWayPointIndex].transform.position,
                    movingSpeed * Time.deltaTime);
            }
            else
            {
                idleTime = 0;
                anim.SetBool("Moving", true);
                movingSpeed = 1f;
                transform.position = Vector2.MoveTowards(transform.position,
                    wayPoints[curWayPointIndex].transform.position,
                    movingSpeed * Time.deltaTime);
                cooldownTimer += Time.deltaTime;
                if (PlayerInSight())
                {
                    if (cooldownTimer >= attackCooldown)
                    {
                        cooldownTimer = 0;
                        anim.SetTrigger("meleeAttack");
                    }
                }
            }
        }
        

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {          
            anim.SetTrigger("meleeAttack");
        }
    }
    private void flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
        anim.SetBool("Moving", false);
        idleTime += Time.deltaTime;
    }
    private bool PlayerInSight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
            0, Vector2.left, 0, playerlayer);

        return hit.collider != null;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }
}
