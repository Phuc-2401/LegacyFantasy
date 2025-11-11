using UnityEngine;
using UnityEngine.SceneManagement;
using Com.LuisPedroFonseca.ProCamera2D;
using System.Collections;

public class GamePlayController : MonoBehaviour
{
    public static GamePlayController instance;
    public PlayerContaint playerContaint;
    public GameScene gameScene;

    public void Awake()
    {
        instance = this;
    }
    public void Start()
    {
        gameScene.Init();
        playerContaint.Init();
    }
}
