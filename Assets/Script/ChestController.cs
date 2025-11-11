using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestController : MonoBehaviour
{
    public Sprite closedChest;
    public Sprite openedChest;
    public GameObject itemPrefab;
    private bool isOpened = false;
    private SpriteRenderer spriteRenderer;
    public Rigidbody2D rb;
    public BoxCollider2D boxCollider;

    public void Start()
    {
        boxCollider.isTrigger = false;
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = closedChest;
    }
    public void OpenChest()
    {
        if (isOpened) return;
        isOpened = true;
        spriteRenderer.sprite = openedChest;
        boxCollider.isTrigger = true;
        if (itemPrefab != null)
        {
            Instantiate(itemPrefab, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
        }
    }
}
