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
                var Map = PlayerPrefs.GetInt("currentMap", 1);
                Map += 1;
                PlayerPrefs.SetInt("currentMap", Map);
                PlayerPrefs.Save();
                SceneManager.LoadScene("GamePlay");
            }
        }
    }
}
