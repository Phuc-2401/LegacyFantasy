using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.LuisPedroFonseca.ProCamera2D;

public class CharacterControllerGamePlay : MonoBehaviour
{
    public ProCamera2D proCamera2D;
    public static CharacterControllerGamePlay instance;
    public PlayerController player;
    public GameObject playerPrefab;
    private Transform startPoint;
    private void Awake()
    {
        instance = this;
    }
    public void Init()
    {
        startPoint = GameObject.FindWithTag("StartPoint")?.transform;
        if (startPoint == null)
        {
            return;
        }

        if (player == null)
        {
            player = Instantiate(playerPrefab, startPoint.position, Quaternion.identity)
                     .GetComponent<PlayerController>();
        }
        else
        {
            player.transform.position = startPoint.position;
        }
        if (proCamera2D == null)
        {
            proCamera2D = FindObjectOfType<ProCamera2D>();
        }
        if (proCamera2D != null)
        {
            proCamera2D.RemoveAllCameraTargets();
            proCamera2D.AddCameraTarget(player.transform);
        }
        
    }
}
