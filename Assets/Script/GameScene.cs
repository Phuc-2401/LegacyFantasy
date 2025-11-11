using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameScene : MonoBehaviour
{
    public int currentStar;
    public int requiredStars;
    public TMP_Text starText;
    public void Init()
    {
    }
    public void Start()
    {
        Target target = FindObjectOfType<Target>();
        requiredStars = target.requiredStars;
        starText.text = currentStar.ToString() + " / " + requiredStars.ToString();
        PlayerPrefs.SetInt("currentStar", 0);
        PlayerPrefs.Save();
    }
    public void UpdateStar(int value)
    {
        currentStar += value;
        Target target = FindObjectOfType<Target>();
        requiredStars = target.requiredStars;
        starText.text = currentStar.ToString() + " / " + requiredStars.ToString();
        PlayerPrefs.SetInt("currentStar", currentStar);
        PlayerPrefs.Save();
    }
}
