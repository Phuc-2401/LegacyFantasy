using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public Button playBtn;
    public Button exitBtn;
    private void Start()
    {
        playBtn.onClick.AddListener(OnClickPlay);
        exitBtn.onClick.AddListener(OnClickExit);
    }
    private void OnClickPlay()
    {
        SceneManager.LoadScene("GamePlay");
    }
    private void OnClickExit()
    {
        Application.Quit();
    }

}
