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
    public MapController mapController;
    private void Awake()
    {
        instance = this;
    }
    public void Init()
    {
        int mapID = PlayerPrefs.GetInt("currentMap", 1);

        var spawn = mapController.spawns.Find(s => s.id == mapID);

        Vector3 spawnPos =  Vector3.zero;
        spawnPos = spawn.post.position;

        player = Instantiate(playerPrefab, spawnPos, Quaternion.identity).GetComponent<PlayerController>();

        proCamera2D.AddCameraTarget(player.transform);
    }
}
