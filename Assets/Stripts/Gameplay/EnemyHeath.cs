using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHeath : MonoBehaviour
{
   
    [Header("Health")]
    [SerializeField] private float startingHealth;
    public float health { get; private set; }
    private Animator anim;
    private bool dead;

    [Header("iFrames")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numberOfFlashes;
    private SpriteRenderer spriteRend;

    private bool invulnerable;

    private void Awake()
    {
        health = startingHealth;
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(float _damage)
    {
        if (invulnerable) return;
        health = Mathf.Clamp(health - _damage, 0, startingHealth);

        if (health > 0)
        {
            anim.SetTrigger("Hit");
            if (AudioManager.HasInstance)
            {
                AudioManager.Instance.PlaySound(AUDIO.Sound_HURT);    
            }

            StartCoroutine(Invunerability());
        }
        else
        {
            if (!dead)
            {
                anim.SetTrigger("Death");
                AudioManager.Instance.PlaySound(AUDIO.Sound_DEATH);
                //Enemy

                if (GetComponent<EnemyMovement>() != null)
                {
                    GetComponent<EnemyMovement>().enabled = false;                    
                }
                if (GetComponent<MeleeEnemy>() != null)
                {
                    GetComponent<MeleeEnemy>().enabled = false;
                }
                if(GetComponent<AIChase>() != null)
                {
                    GetComponent<AIChase>().enabled = false;
                }
                if(GetComponent<BoxCollider2D>() != null)
                {
                    GetComponent<BoxCollider2D>().enabled = false;
                }
                dead = true;
            }
        }
    }
    
    private IEnumerator Invunerability()
    {
        invulnerable = true;
        Physics2D.IgnoreLayerCollision(10, 11, true);
        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRend.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
        }
        Physics2D.IgnoreLayerCollision(10, 11, false);
        invulnerable = false;
    }
    public void destroyEvent()
    {
        
        Destroy(gameObject);
    }
}
