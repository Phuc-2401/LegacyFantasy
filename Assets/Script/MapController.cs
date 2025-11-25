using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapController : MonoBehaviour
{
    public GameObject mapData;
    public List<Spawn> spawns;
    Spawn Spawn(int id)
    { 
            foreach (var item in spawns)
            {
                if (item.id == id)
                {
                    return item;
                }
            }
            return null;
    }    
    public void Init()
    {
        string pathMap = "Map{0}";
        int idLevel = PlayerPrefs.GetInt("currentMap", 1);
        mapData = Instantiate(Resources.Load<GameObject>(string.Format(pathMap, idLevel)));
        mapData.transform.position = Vector3.zero;
        var temp = Spawn(idLevel);
    }
}
[System.Serializable]
public class Spawn 
{
    public int id;
    public Transform post;
}
