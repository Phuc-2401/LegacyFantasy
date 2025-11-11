using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Enemies : MonoBehaviour
{
    public string idleAnim;
    public string runAnim;
    public string attackAnim;
    public string hitAnim;
    public string dieAnim;
    public Animator animator;
    public Rigidbody2D rb;
    public float speed;
    public bool wasRotated = false;

    public float patrolDistance; 
    private Vector3 startPos;

    public bool isChasingPlayer = false;
    public Transform playerTransform;

    public float attackRange;
    public bool isAttacking = false;
    public float attackDuration = 1.5f;
    public bool isFacingRight = true;

    public int maxHp;
    public int currentHp;
    public Image healthBar;
    public bool isDead = false;

    public Transform starPrefab;


    void Start()
    {
        currentHp = maxHp;
        UpdateHealthBar();
        startPos = transform.position;
         wasRotated = transform.localScale.x < 0;
    }

    void Update()
    {
        if(isDead) return;

        if (isChasingPlayer && playerTransform != null)
        {
            ChasePlayer();
        }
        else
        {
            Move();
        }


    } 
    
    public void Move()
    {
        float moveDir = Mathf.Sign(transform.localScale.x);
        transform.position += new Vector3(moveDir * speed * Time.deltaTime, 0, 0);
        animator.Play(runAnim);


        float distanceFromStart = transform.position.x - startPos.x;

        if ( !wasRotated && distanceFromStart >= patrolDistance)
        {

            TurnAround();
        }
        else if (wasRotated && distanceFromStart <= -patrolDistance)
        {
            TurnAround();
        }
    }
    public void ChasePlayer()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);

        if (distanceToPlayer > attackRange)
        {
            Vector3 dir = (playerTransform.position - transform.position).normalized;
            transform.position += new Vector3(dir.x * speed * Time.deltaTime, 0, 0);
            animator.Play(runAnim);

            if (dir.x > 0 && wasRotated)
                TurnAround();
            else if (dir.x < 0 && !wasRotated)
                TurnAround();
        }
        else
        {
            if (!isAttacking)
                StartCoroutine(Attack());
        }
    }
    void TurnAround()
    {
        wasRotated = !wasRotated;

        
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
    public IEnumerator Attack()
    {

        isAttacking = true;
        animator.Play(attackAnim);
       

        rb.velocity = Vector2.zero;
        rb.bodyType = RigidbodyType2D.Kinematic;

        yield return new WaitForSeconds(attackDuration);
        rb.bodyType = RigidbodyType2D.Dynamic;
        isAttacking = false;
        

    }
    public void StartChase(Transform target)
    {
        playerTransform = target;
        isChasingPlayer = true;
    }

    public void StopChase()
    {
        playerTransform = null;
        isChasingPlayer = false;
    }
    public void ForceTurnLeft()
    {
        wasRotated = true;
        Vector3 scale = transform.localScale;
        scale.x = -Mathf.Abs(scale.x);
        transform.localScale = scale;
    }

    public void ForceTurnRight()
    {
        wasRotated = false;
        Vector3 scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x);
        transform.localScale = scale;
    }
    public void TakeDamage(int damage)
    {
        if (isDead) return;
        animator.Play(hitAnim);
        currentHp -= damage;
        UpdateHealthBar();
        
        if (currentHp <= 0)
        {
            Die();
        }

    }
   

    public void UpdateHealthBar()
    {
        if (healthBar != null)
            healthBar.fillAmount = (float)currentHp / maxHp;

    }
    public void Die()
    {
        isDead = true;
        animator.Play(dieAnim);
        if(starPrefab != null)
        {
            Instantiate(starPrefab, transform.position, Quaternion.identity);
        }
        Destroy(gameObject, 1f);
    }
    public void DealDamage()
    {
        if (isDead || playerTransform == null) return;

        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);
        if (distanceToPlayer <= attackRange)
        {
            PlayerController playerController = playerTransform.GetComponent<PlayerController>();
            if (playerController != null)
            {
                Debug.Log("Enemy deals damage to player!");
                playerController.TakeDamagePlayer(5); // tùy chỉnh damage
            }
        }
    }



}
