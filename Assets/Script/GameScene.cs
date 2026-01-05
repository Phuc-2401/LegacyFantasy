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
    public GameObject nextMapPanel;
    public GameObject losePanel;
    public GameObject winPanel;
    public GameObject toturialPanel;
    public GameObject pausePanel;
    public Button Continue;
    public Button Pause;
    public Button Retry;
    public Button Next;
    public Button Menu;
    public Button Menu2;
    public Button pauseRetry;
    public Button pauseMenu;

    public void Init()
    {
        Next.onClick.AddListener(OnClickNext);
        Retry.onClick.AddListener(OnClickRetry);
        Menu.onClick.AddListener(OnClickMenu);
        Menu2.onClick.AddListener(OnClickMenu);
        Pause.onClick.AddListener(OnClickPause);
        Continue.onClick.AddListener(OnClickContinue);
        pauseRetry.onClick.AddListener(OnClickRetry);
        pauseMenu.onClick.AddListener(OnClickMenu);
    }
    private void OnClickContinue()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
    }
    private void OnClickPause()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
    }
    private void OnClickMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("PlayGameScene");
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
    }
    private void OnClickRetry()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("GamePlay");
    }
    private void OnClickNext()
    {
        Time.timeScale = 1f;
        var Map = PlayerPrefs.GetInt("currentMap", 1);
        Map += 1;
        PlayerPrefs.SetInt("currentMap", Map);
        PlayerPrefs.Save();
        SceneManager.LoadScene("GamePlay");
    }
    public void Start()
    {
        if (PlayerPrefs.GetInt("currentMap", 1) == 1)
        {
            toturialPanel.SetActive(true);
        }
        else
        {
            toturialPanel.SetActive(false);

        }
        starBar.SetActive(true);
        Target target = FindObjectOfType<Target>();
        requiredStars = target.requiredStars; 
        starText.text = currentStar.ToString() + " / " + requiredStars.ToString();
        PlayerPrefs.SetInt("currentStar", 0);
        PlayerPrefs.Save();
    }
    public void UpdateStar(int value)
    {
        Target target = FindObjectOfType<Target>();
        requiredStars = target.requiredStars;

        currentStar += value;

        if (currentStar > requiredStars)
        {
            currentStar = requiredStars;
        }

        starText.text = currentStar + " / " + requiredStars;
        PlayerPrefs.SetInt("currentStar", currentStar);
    }
}
