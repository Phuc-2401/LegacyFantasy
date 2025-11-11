using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackZone : MonoBehaviour
{
    public Enemies enemyScript;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            enemyScript.StartChase(other.transform);
            float dir = other.transform.position.x - enemyScript.transform.position.x;

            if (dir > 0 && enemyScript.transform.localScale.x < 0)
            {
                enemyScript.ForceTurnRight();
            }
            else if (dir < 0 && enemyScript.transform.localScale.x > 0)
            {
                enemyScript.ForceTurnLeft();
            }

        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            enemyScript.StopChase();
        }
    }
}
