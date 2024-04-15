using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stall : MonoBehaviour
{
    private GameObject player;
    private GameObject inventory;
    private SpriteRenderer stallSpriteRenderer;
    private bool allowClick = false;

    void Start()
    {
        stallSpriteRenderer = GetComponent<SpriteRenderer>();
        player = GameObject.FindWithTag("Player");
        inventory = GameObject.FindWithTag("Inventory");
    }

    void Update()
    {
        
    }

    void OnMouseDown()
    {
        if (allowClick)
        {
            inventory.GetComponent<Menu>().openShop();
        }
    }

    private void FixedUpdate()
    {
        float bottomY = transform.position.y - stallSpriteRenderer.bounds.extents.y; 
        float playerY = player.transform.position.y;

        if (playerY < bottomY + 0.08f && bottomY < playerY) 
        {
            allowClick = true;
            stallSpriteRenderer.sprite = Resources.Load<Sprite>("Tools/Shop");
        }
        else
        {
            allowClick = false;
            stallSpriteRenderer.sprite = Resources.Load<Sprite>("Tools/ShopEmpty");
        }
    }
}
