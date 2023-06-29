using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour
{
   
    private Animator anim;
    private bool dead;

    [Header("iFrames")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numberOfFlashes;
    private SpriteRenderer spriteRend;
    private float startingHealth;
    private float currentHealth;


    private bool invulnerable;

    private void Awake()
    {
        startingHealth = GameManager.Instance.startingHealth;
        currentHealth = GameManager.Instance.currentHealth;
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
            TakeDamage(1);
        }       
    }
    public void TakeDamage(float _damage)
    {
        if (invulnerable) return;
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);
        GameManager.Instance.SetHealth(currentHealth);
        if (currentHealth > 0)
        {
            anim.SetTrigger("Hurt");           
            AudioManager.Instance.PlaySound(AUDIO.Sound_HURT);            
            StartCoroutine(Invunerability());
        }
        else
        {
            if (!dead)
            {
                anim.SetTrigger("Death");
                AudioManager.Instance.PlaySound(AUDIO.Sound_DEATH);

                //Player
                if (GetComponent<HeroKnight>() != null)
                {
                    GetComponent<HeroKnight>().enabled= false; 
                }
                
                dead = true;                
            }
                if (UIManager.HasInstance && dead == true)
                {
                    UIManager.Instance.ActiveLosingPanel(true);                
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
}