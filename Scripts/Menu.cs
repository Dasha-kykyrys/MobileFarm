using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public GameObject inventoryPrefab;
    public GameObject deskPrefab;
    public GameObject shopPrefab;
    private GameObject controller;

    public bool inventoryOpened;
    public bool deskOpened;
    public bool shopOpened;
    private GameObject inventory;
    private GameObject desk;
    private GameObject shop;

    void Start()
    {
        controller = GameObject.FindWithTag("GameController");
    }

    public void openInventory()
    {
        if (deskOpened)
        {
            controller.SetActive(true);
            Destroy(desk);
            deskOpened = false;
        }
        else if (shopOpened)
        {
            controller.SetActive(true);
            Destroy(shop);
            shopOpened = false;
        }
        else
        {
             if (inventoryOpened)
            {
                controller.SetActive(true);
                Destroy(inventory);
                inventoryOpened = false;
            }
            else
            {
                controller.SetActive(false);
                inventory = Instantiate(inventoryPrefab);
                inventory.transform.SetParent(gameObject.transform);
                inventory.GetComponent<RectTransform>().offsetMin = new Vector2(100, 59);
                inventory.GetComponent<RectTransform>().offsetMax = new Vector2(-100, -50);
                inventory.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);

                inventoryOpened = true;
            }
        }
       
    }

    public void openShop()
    {
        if(!inventoryOpened && !deskOpened && !shopOpened)
        {
            controller.SetActive(false);
            shop = Instantiate(shopPrefab);
            shop.transform.SetParent(gameObject.transform);
            shop.GetComponent<RectTransform>().offsetMin = new Vector2(100, 59);
            shop.GetComponent<RectTransform>().offsetMax = new Vector2(-100, -50);
            shop.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);

            shopOpened = true;
        }
    }

    public void openDesk()
    {
        if(!inventoryOpened && !deskOpened && !shopOpened)
        {
            controller.SetActive(false);
            desk = Instantiate(deskPrefab);
            desk.transform.SetParent(gameObject.transform);
            desk.GetComponent<RectTransform>().offsetMin = new Vector2(100, 59);
            desk.GetComponent<RectTransform>().offsetMax = new Vector2(-100, -59);
            desk.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);

            deskOpened = true;
        }
    }
}
