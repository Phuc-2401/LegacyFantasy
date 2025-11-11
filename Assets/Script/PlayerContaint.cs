using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerContaint : MonoBehaviour
{
    public CharacterControllerGamePlay characterController;
    public MapController mapController;
    public void Init()
    {
        mapController.Init();
        characterController.Init();
    }
}
