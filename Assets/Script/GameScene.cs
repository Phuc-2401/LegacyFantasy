using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameScene : MonoBehaviour
{
    public int currentStar;
    public int totalStar;
    public TMP_Text starText;

    public void Init()
    {
        
    }
    public void Awake()
    {
        
    }
    public void Start()
    {
        starText.text = currentStar.ToString() + " / " + totalStar.ToString();
    }
    public void UpdateStar(int value)
    {
        currentStar += value;
        starText.text = currentStar.ToString() + " / " + totalStar.ToString();
    }
    public void ResetStar(int total)
    {
        currentStar = 0;
        totalStar = total;
        if (starText != null)
        {
            starText.text = currentStar.ToString() + " / " + totalStar.ToString();
        }
    }
}
