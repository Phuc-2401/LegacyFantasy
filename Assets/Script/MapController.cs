using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapController : MonoBehaviour
{
    public static MapController instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void Init()
    {
    }
    public void LoadMap(string mapName)
    {
        SceneManager.LoadScene(mapName);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Map loaded: " + scene.name);
        Target target = FindObjectOfType<Target>();
        int requiredStars;
        if (target != null)
        {
            requiredStars = target.requiredStars;
        }
        else
        {
            requiredStars = 0;
        }

        GameScene gameScene = FindObjectOfType<GameScene>();
        if (gameScene != null)
        {
            gameScene.ResetStar(requiredStars);
        }
        InitAfterSceneLoad();
    }

    private void InitAfterSceneLoad()
    {
        if (GamePlayController.instance != null)
            GamePlayController.instance.Init();
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
