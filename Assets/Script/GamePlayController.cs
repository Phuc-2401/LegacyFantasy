using UnityEngine;
using UnityEngine.SceneManagement;
using Com.LuisPedroFonseca.ProCamera2D;
using System.Collections;

public class GamePlayController : MonoBehaviour
{
    public static GamePlayController instance;
    public PlayerContaint playerContaint;
    public GameScene gameScene;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void Init()
    {
        if (gameScene != null)
            gameScene.Init();

        if (playerContaint != null)
            playerContaint.Init();
    }
}
