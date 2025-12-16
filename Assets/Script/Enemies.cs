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

    public float patrolDistance; 
    private Vector3 startPos;

    public bool isChasingPlayer = false;
    public Transform playerTransform;

    public float attackRange;
    public bool isAttacking = false;
    public float attackDuration = 1.5f;
    public bool isFacingRight = true;
    public float detectRange;
    public float outRange;

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
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if(isDead) return;

        DetectPlayer();

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

        if (Mathf.Abs(distanceFromStart) >= patrolDistance)
        {
            TurnAround();
        }
    }
    public void DetectPlayer()
    {
        if (playerTransform == null) return;

        float distance = Vector2.Distance(transform.position, playerTransform.position);

        if (distance <= detectRange)
        {
            isChasingPlayer = true;
        }
        else if (distance > outRange)
        {
            isChasingPlayer = false;
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

            if (Mathf.Sign(dir.x) != Mathf.Sign(transform.localScale.x))
            {
                TurnAround();
            }

        }
        else
        {
            if (!isAttacking)
                StartCoroutine(Attack());
        }
    }
    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);

        Gizmos.color = Color.gray;
        Gizmos.DrawWireSphere(transform.position, outRange);
    }
    public void TurnAround()
    {
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
        int currentMap = PlayerPrefs.GetInt("currentMap", 1);
        if (isDead || playerTransform == null) return;

        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);
        if (distanceToPlayer <= attackRange)
        {
            PlayerController playerController = playerTransform.GetComponent<PlayerController>();
            if (playerController != null && currentMap == 1)
            {
                Debug.Log("Enemy deals damage to player = 5!");
                playerController.TakeDamagePlayer(5); 
            }
            if(currentMap == 2)
            {    
                Debug.Log("Enemy deals damage to player = 10!");
                playerController.TakeDamagePlayer(10);
            }
            if(currentMap == 3)
            {
                Debug.Log("Enemy deals damage to player = 15!");
                playerController.TakeDamagePlayer(15);
            }
        }
    }



}
