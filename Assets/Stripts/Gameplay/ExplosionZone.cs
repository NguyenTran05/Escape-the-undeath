using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionZone : MonoBehaviour
{
    [SerializeField] private float colliderDistance;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private float range;
    private void Update()
    {
        DeathZone();
        
    }
    private bool DeathZone()
    {
        
        RaycastHit2D hit =
            Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
            0, Vector2.left, 0, playerLayer);
        if (hit.collider != null)
            hit.transform.GetComponent<Health>().TakeDamage(10);

        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
        if (AudioManager.HasInstance)
        {
            AudioManager.Instance.PlaySound(AUDIO.Sound_FIRETRAP);
        }

    }
   
}
