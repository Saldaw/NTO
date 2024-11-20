using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    //--------------------Unity UI переменные--------------------\\
    [SerializeField] private TextMeshProUGUI cost1Text;
    [SerializeField] private TextMeshProUGUI count1Text;
    [SerializeField] private Button buttonBuy1;

    [SerializeField] private TextMeshProUGUI cost2Text;
    [SerializeField] private TextMeshProUGUI count2Text;
    [SerializeField] private Button buttonBuy2;

    [SerializeField] private TextMeshProUGUI cost3Text;
    [SerializeField] private TextMeshProUGUI count3Text;
    [SerializeField] private Button buttonBuy3;

    [SerializeField] private TextMeshProUGUI cost4Text;
    [SerializeField] private TextMeshProUGUI count4Text;
    [SerializeField] private Button buttonBuy4;

    [SerializeField] private TextMeshProUGUI cost5Text;
    [SerializeField] private TextMeshProUGUI count5Text;
    [SerializeField] private Button buttonBuy5;
    //-----------------------------------------------------------\\

    [SerializeField] readonly private Dictionary<string, Image> imagesForShops;
    [SerializeField] private List<Image> imagesForResourses;

    private ShopController shopCR;
    public void UpdateInfo(Shop info)
    {
        buttonBuy1.enabled = info.resources.Resurse1.count > 0;
        buttonBuy2.enabled = info.resources.Resurse2.count > 0;
        buttonBuy3.enabled = info.resources.Resurse3.count > 0;
        buttonBuy4.enabled = info.resources.Resurse4.count > 0;
        buttonBuy5.enabled = info.resources.Resurse5.count > 0;

        cost1Text.text = info.resources.Resurse1.cost.ToString();
        count1Text.text = info.resources.Resurse1.count.ToString();
        cost2Text.text = info.resources.Resurse2.cost.ToString();
        count2Text.text = info.resources.Resurse2.count.ToString();
        cost3Text.text = info.resources.Resurse3.cost.ToString();
        count3Text.text = info.resources.Resurse3.count.ToString();
        cost4Text.text = info.resources.Resurse4.cost.ToString();
        count4Text.text = info.resources.Resurse4.count.ToString();
        cost5Text.text = info.resources.Resurse5.cost.ToString();
        count5Text.text = info.resources.Resurse5.count.ToString();

    }
    private void Start()
    {
        shopCR = GetComponent<ShopController>();
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
    private void Open(string shopName)
    {
        Shop shop = shopCR.GetShop(shopName);
        if (shop == null)
        {
            UpdateInfo(shop);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
