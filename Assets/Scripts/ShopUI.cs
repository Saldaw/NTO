using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ShopUI : MonoBehaviour
{
    private ShopController shopCR;
    public void UpdateInfo(Shop info)
    {

    }
    private void Start()
    {
        shopCR = GetComponent<ShopController>();
        shopCR.GetShop("testShop");
    }
    // Start is called before the first frame update
    public void fdfd()
    {
        
        ShopResources resources = new ShopResources();
        resources.Resurse1 = new ResourceInShop() { cost = 5, count = 10 };
        resources.Resurse2 = new ResourceInShop() { cost = 0, count = 0 };
        resources.Resurse3 = new ResourceInShop() { cost = 0, count = 0 };
        resources.Resurse4 = new ResourceInShop() { cost = 0, count = 0 };
        resources.Resurse5 = new ResourceInShop() { cost = 0, count = 0 };
        shopCR.BuyResourse(resources, "testShop");
    }
    private void Open()
    {
        Shop shop = shopCR.GetShop("1");
        if (shop == null)
        {

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
