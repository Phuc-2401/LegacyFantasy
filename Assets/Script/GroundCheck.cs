using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public PlayerController playerController;
    private void OnTriggerStay2D(Collider2D collision)
    {
        playerController.groundCheck = true;

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        playerController.groundCheck = false;
    }
}
