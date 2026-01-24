using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed;
    public int damage;
    private Vector2 direction;

    public void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            direction = (player.transform.position - transform.position).normalized;
        }
        Destroy(gameObject, 3f);
    }

    public void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerController>()?.TakeDamagePlayer(damage);
            Destroy(gameObject);
        }
        if(collision.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
}
