using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
public enum EnemyState
{
    Patrol,
    Shoot,
    Chase,
    Attack
}
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

    public Transform playerTransform;

    public float attackRange;
    public bool isAttacking = false;
    public float detectRange;
    public float distanceToPlayer;

    public int maxHp;
    public int currentHp;
    public Image healthBar;
    public bool isDead = false;

    public Transform starPrefab;

    public float bulletRange;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireRate;

    private float nextFireTime;
    private EnemyState currentState;
    public void Start()
    {
        currentHp = maxHp;
        UpdateHealthBar();
        startPos = transform.position;
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void Update()
    {
        if (isDead) return;
        if (playerTransform == null) return;

        if (playerTransform != null)
        {
            distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);
        }
        if (distanceToPlayer <= attackRange)
            currentState = EnemyState.Attack;
        else if (distanceToPlayer <= detectRange)
            currentState = EnemyState.Chase;
        else if (distanceToPlayer <= bulletRange)
            currentState = EnemyState.Shoot;
        else
            currentState = EnemyState.Patrol;

        switch (currentState)
        {
            case EnemyState.Patrol:
                Move();
                break;
            case EnemyState.Chase:
                ChasePlayer();
                break;
            case EnemyState.Shoot:
                FacePlayer();
                ShootPlayer();
                break;
            case EnemyState.Attack:
                FacePlayer();
                if (!isAttacking)
                    StartCoroutine(Attack());
                break;
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
    public void ChasePlayer()
    {
        Vector3 dir = (playerTransform.position - transform.position).normalized;
        transform.position += new Vector3(dir.x * speed * Time.deltaTime, 0, 0);

        FacePlayer();

        animator.Play(runAnim);
    }
    public void FacePlayer()
    {
        if (playerTransform == null) return;

        float directionX = playerTransform.position.x - transform.position.x;
        if (Mathf.Abs(directionX) > 0.01f)
        {
            Vector3 scale = transform.localScale;
            scale.x = Mathf.Abs(scale.x) * Mathf.Sign(directionX);
            transform.localScale = scale;
        }
    }
    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, bulletRange);
    }
    public void TurnAround()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
    IEnumerator Attack()
    {

        isAttacking = true;
        animator.Play(attackAnim);
        rb.velocity = Vector2.zero;
        rb.bodyType = RigidbodyType2D.Kinematic;
        yield return new WaitForSeconds(1f);
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
        Destroy(gameObject, 1f);
        if (starPrefab != null)
        {
            Instantiate(starPrefab, transform.position, Quaternion.identity);
        }
        
    }
    public void DealDamage()
    {
        int currentMap = PlayerPrefs.GetInt("currentMap", 1);
        if (isDead || playerTransform == null) return;

        if (distanceToPlayer <= attackRange)
        {
            PlayerController playerController = playerTransform.GetComponent<PlayerController>();
            if (playerController != null)
            {
                switch (currentMap)
                {
                    case 1:
                        Debug.Log("Enemy deals damage to player = 5!");
                        playerController.TakeDamagePlayer(5);
                        break;

                    case 2:
                        Debug.Log("Enemy deals damage to player = 10!");
                        playerController.TakeDamagePlayer(10);
                        break;

                    case 3:
                        Debug.Log("Enemy deals damage to player = 15!");
                        playerController.TakeDamagePlayer(15);
                        break;
                }
            }
        }
    }
    public void ShootPlayer()
    {
        if (Time.time < nextFireTime) return;

        nextFireTime = Time.time + fireRate;
        animator.Play(idleAnim);
        Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
    }


}
