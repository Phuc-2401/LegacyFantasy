using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum ActionType
{
    Left,
    Right,
    Jump,
    Attack
}


public class PlayerController : MonoBehaviour
{
    public string idleAnim;
    public string runAnim;
    public string jumpAnim;
    public string attackAnim;
    public Animator animator;

    public Rigidbody2D rb;
    public float speed;

    public bool groundCheck;
    public float jumpForce;
    public SpriteRenderer spriteRender;
    public float attackDuration = 1f;
    private bool isAttacking = false;

    public int playerCurrentHp;
    public int playerMaxHp;
    public Image healthBarPlayer;
    public bool isDead = false;
    private bool isHurt = false;

    public Collider2D hitBox;
    private Coroutine dotCoroutine;

    public void Start()
    {
        playerCurrentHp = playerMaxHp;
        UpdateHealthBar();
        if (hitBox != null )
        {
            hitBox.enabled = false;
           
        }
    }
    public virtual void Update()
    {
        if (isDead || isAttacking || isHurt) return;
        if (isAttacking)
        {
            return;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            
            Move(ActionType.Left);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            
            Move(ActionType.Right);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            
            Move(ActionType.Jump);
        }
        if (Input.GetKeyDown(KeyCode.Q) && !isAttacking)
        {
            StartCoroutine(Attack());
        }

        if (!Input.anyKey)
        {
            if (groundCheck) // cham dat
            {
                rb.velocity = new Vector2(0,rb.velocity.y);
                animator.Play(idleAnim);
            }
            else
            {
                animator.Play(jumpAnim);
            }
        }
        if (!groundCheck)
        {
            animator.Play(jumpAnim);
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                spriteRender.transform.localScale = new Vector3(-1, 1, 1);
                rb.velocity = new Vector2(-speed, rb.velocity.y);
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                spriteRender.transform.localScale = new Vector3(1, 1, 1);
                rb.velocity = new Vector2(speed, rb.velocity.y);
            }

        }
    }
    public virtual void Move(ActionType actionTypeParam)
    {
        if (!groundCheck)
        {
            return;
        }

        switch (actionTypeParam)
        {
            case ActionType.Left:

                rb.velocity = new Vector2(-speed, rb.velocity.y);
                spriteRender.transform.localScale = new Vector3(-1, 1, 1);
                if (groundCheck)
                {
                    animator.Play(runAnim);
                }
                else
                {
                    animator.Play(jumpAnim);
                }


                break;
            case ActionType.Right:
                rb.velocity = new Vector2(speed, rb.velocity.y);
                spriteRender.transform.localScale = new Vector3(1, 1, 1);
                if (groundCheck)
                {
                    animator.Play(runAnim);
                }
                else
                {
                    animator.Play(jumpAnim);
                }
                break;
            case ActionType.Jump:
                animator.Play(jumpAnim);
                rb.AddForce(new Vector2(rb.velocity.x, 2 * jumpForce), ForceMode2D.Impulse);
                break;
        }
      

    }
    IEnumerator Attack()
    {
        isAttacking = true;
        rb.velocity = new Vector2(0, rb.velocity.y);
 
        animator.Play(attackAnim);
        hitBox.enabled = true;
        yield return new WaitForSeconds(attackDuration);
        hitBox.enabled = false;
        isAttacking = false;
    }
    public void TakeDamagePlayer(int damage)
    {
        
        if (isDead) return;
        playerCurrentHp -= damage;
        UpdateHealthBar();

        if (playerCurrentHp <= 0)
        {
            
            Die();
            return;
        }
        StartCoroutine(PlayHitAnimation());

    }
    public void HealPlayer(int healAmount)
    {
        if (isDead) return;
        playerCurrentHp += healAmount;
        if (playerCurrentHp > playerMaxHp)
        {
            playerCurrentHp = playerMaxHp;
        }
        UpdateHealthBar();
    }
    public void UpdateHealthBar()
    {
        if (healthBarPlayer != null) {
            healthBarPlayer.fillAmount = (float)playerCurrentHp / playerMaxHp;
        }
    }

    public void Die()
    {
        isDead = true;
        animator.ResetTrigger("Hit");
        animator.SetTrigger("Dead");
        CharacterControllerGamePlay.instance.PlayerDead();
        GamePlayController.instance.gameScene.losePanel.SetActive(true);
        Destroy(gameObject, 0.6f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(isAttacking&& collision.gameObject.CompareTag("Enemy"))
        {
            Enemies enemy = collision.gameObject.GetComponent<Enemies>();
            if (enemy != null)
            {
                enemy.TakeDamage(10);
            }
        }
        if(isAttacking && collision.gameObject.CompareTag("Chest"))
        {
            ChestController chest = collision.gameObject.GetComponent<ChestController>();
            if (chest != null)
            {
                chest.OpenChest();
            }
        }
        if(collision.gameObject.tag == "HealthItem")
        {
            HealPlayer(20);
            Destroy(collision.gameObject);
        }
    }
    public void EnableHitBox()
    {
        hitBox.enabled = true;
    }
    public void DisableHitBox()
    {
        hitBox.enabled = false;
    }
    private IEnumerator PlayHitAnimation()
    {
        if(isDead) yield break;
        isHurt = true;
        animator.SetTrigger("Hit");

        yield return new WaitForSeconds(0.5f);

        isHurt = false;
    }
    public void DamageOverTime(int totalDamage, float duration)
    {
        dotCoroutine = StartCoroutine(DOT(totalDamage, duration));
    }
    public void StopDamageOverTime()
    {
        StopCoroutine(dotCoroutine);
    }
    IEnumerator DOT(int totalDamage, float duration)
    {
        int ticks = 20; 
        float delay = duration / ticks;
        int dmg = totalDamage / ticks;

        for (int i = 0; i < ticks; i++)
        {
            if (isDead) yield break;
            TakeDamagePlayer(dmg);
            yield return new WaitForSeconds(delay);
        }
    }
}
