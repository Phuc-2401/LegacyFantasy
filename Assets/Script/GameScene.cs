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
    public GameObject starBar;
    public GameObject continuePanel;
    public GameObject losePanel;
    public GameObject playGamePanel;
    public Button Retry;
    public Button Continue;
    public Button Play;
    public Button Exit;

    public void Init()
    {
        Continue.onClick.AddListener(OnClickNext);
        Retry.onClick.AddListener(OnClickRetry);
        Play.onClick.AddListener(OnClickPlay);
        Exit.onClick.AddListener(OnClickExit);
    }
    private void OnClickPlay()
    {
        playGamePanel.SetActive(false);
        starBar.SetActive(true);
    }
    private void OnClickExit()
    {
        Application.Quit();
    }
    private void OnClickRetry()
    {
        SceneManager.LoadScene("GamePlay");
    }
    private void OnClickNext()
    {
        var Map = PlayerPrefs.GetInt("currentMap", 1);
        Map += 1;
        PlayerPrefs.SetInt("currentMap", Map);
        PlayerPrefs.Save();
        SceneManager.LoadScene("GamePlay");
    }
    public void Start()
    {
        starBar.SetActive(false);
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
