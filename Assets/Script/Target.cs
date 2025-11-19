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
            int currentMap = PlayerPrefs.GetInt("currentMap", 1);
            if (currentStar >= requiredStars)
            {
                if(currentMap == 3)
                {
                    GamePlayController.instance.gameScene.winPanel.SetActive(true);
                }
                else
                {
                    GamePlayController.instance.gameScene.continuePanel.SetActive(true);
                }
            }
        }
    }
}
