using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Target : MonoBehaviour
{
    public string nextSceneName;
    public int requiredStars ;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) return;

        if (GamePlayController.instance != null && GamePlayController.instance.gameScene != null)
        {
            var gameScene = GamePlayController.instance.gameScene;
            if (gameScene.currentStar >= requiredStars)
            {
                MapController.instance.LoadMap(nextSceneName);
            }
        }
        else
        {
            MapController.instance.LoadMap(nextSceneName);
        }
    }
}
