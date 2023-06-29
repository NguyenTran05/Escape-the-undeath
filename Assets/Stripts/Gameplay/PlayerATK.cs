using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class PlayerATK : MonoBehaviour
{
    public float damage;

    [SerializeField] float range;
    [SerializeField] float colliderDistance;
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] BoxCollider2D boxCollider;
    [SerializeField] private bool flip;
    private EnemyHeath enemyHealth;
    private HeroKnight player;
    private int direction;
    [SerializeField] private bool Isshow;

    private void Start()
    {
        player = GetComponent<HeroKnight>();
    }
    private bool EnemyInSight()
    {
        if (player.flip)
        {
            direction = -1;
        }
        else
        {
            direction = 1;
        }
             
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance * direction,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
            0, Vector2.left, 0, enemyLayer);
                 
        if (hit.collider != null)
            enemyHealth = hit.transform.GetComponent<EnemyHeath>();

        return hit.collider != null;
    }
    private void OnDrawGizmos()
    {
        if(Isshow == false)
        {
            return;
        }
        if (player.flip)
        {
            direction = -1;
        }
        else
        {
            direction = 1;
        }

        Gizmos.color = Color.red;        
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance * direction,
        new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }
    private void DamageEnemyTake()
    {
        if (EnemyInSight())
        {
            enemyHealth.TakeDamage(damage);
        }
    }
}
