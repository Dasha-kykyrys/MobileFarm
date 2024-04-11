using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    private List<ShopItem> allItems = new List<ShopItem>();
    private List<Item> allProductList = new List<Item>();
    private Text moneyText;

    void Start()
    {
        moneyText = GetComponentsInChildren<Text>()[0];
        moneyText.text = Player.money + "$"; 

        allItems = gameObject.GetComponentsInChildren<ShopItem>().ToList();

        fillAllProducts();
        StartCoroutine(fillShop());
    }


    private IEnumerator fillShop()
    {
        yield return new WaitForSeconds(0.3f);
        for (int i = 0; i < allItems.Count; i++)
        {
            allItems[i].UpdateItem(allProductList[i], moneyText);
        }
    }

    private void fillAllProducts()
    {
        allProductList.Clear();
        allProductList.Add(new Item("carrot", "Food/carrot", 1, Item.TYPEPFOOD, 10, 1, 25f));
        allProductList.Add(new Item("beet", "Food/beet", 1, Item.TYPEPFOOD, 25, 2, 30f));
        allProductList.Add(new Item("pumpkin", "Food/pumpkin", 1, Item.TYPEPFOOD, 35, 3, 40f));
        allProductList.Add(new Item("eggplant", "Food/eggplant", 1, Item.TYPEPFOOD, 40, 4, 50f));
        allProductList.Add(new Item("plow", "Tools/Plow", 1, Item.TYPEPLOW, 10, 1, 0f));
    }
}
