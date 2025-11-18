using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Target : MonoBehaviour
{
    public int requiredStars;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            int currentStar = PlayerPrefs.GetInt("currentStar", 0);
            if (currentStar >= requiredStars)
            {
                GamePlayController.instance.gameScene.continuePanel.SetActive(true);
            }
        }
    }
}
