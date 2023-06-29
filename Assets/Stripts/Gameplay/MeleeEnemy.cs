using System;
using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    [Header("Attack Parameters")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;
    [SerializeField] private int damage;

    [Header("Collider Parameters")]
    [SerializeField] private float colliderDistance;
    [SerializeField] private BoxCollider2D boxCollider;

    [Header("Player Layer")]
    [SerializeField] private LayerMask playerLayer;
    private float cooldownTimer = Mathf.Infinity;

    [Header("Attack Sound")]
    [SerializeField] private AudioClip atksound;

    //References
    private Animator anim;
    private Health playerHealth;
    private EnemyMovement enemyMovement;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        enemyMovement = GetComponent<EnemyMovement>();
    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime;

        //Attack only when player in sight?
        if (PlayerInSight())
        {
            enemyMovement.enabled = false;
            if (cooldownTimer >= attackCooldown)
            {
                cooldownTimer = 0;
                anim.Rebind();
                anim.SetTrigger("meleeAttack");
                AudioManager.Instance.PlaySound(AUDIO.Sound_ATTACK1);

            }

        }
        else
        {
            if (enemyMovement)
            {
                enemyMovement.enabled = true;
            }
        }
    }

    private bool PlayerInSight()
    {
        RaycastHit2D hit =
            Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
            0, Vector2.left, 0, playerLayer);

        if (hit.collider != null)
            playerHealth = hit.transform.GetComponent<Health>();

        return hit.collider != null;
    }
    private void OnDrawGizmos()
    {       
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }
   
    private void DamagePlayerTake()
    {
        if (PlayerInSight())
        {
            playerHealth.TakeDamage(damage);
        }
    }
}