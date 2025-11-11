using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
   public void OnTriggerEnter2D(Collider2D other)
   {
       if (other.gameObject.tag == "Player")
       {
           GamePlayController.instance.gameScene.UpdateStar(1);
           Destroy(this.gameObject);
        }
    }
}
