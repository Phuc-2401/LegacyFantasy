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
        int currentMap = PlayerPrefs.GetInt("currentMap", 1);
        if (collision.CompareTag("Player"))
        {
            switch(currentMap)
            {
                case 1:
                    damage = 5;
                    Debug.Log("Bullet deals damage to player = 5!");
                    break;
                case 2:
                    damage = 10;
                    Debug.Log("Bullet deals damage to player = 10!");
                    break;
                case 3:
                    damage = 15;
                    Debug.Log("Bullet deals damage to player = 15!");
                    break;
            }
            collision.GetComponent<PlayerController>()?.TakeDamagePlayer(damage);
            Destroy(gameObject);
        }
        if(collision.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
}
