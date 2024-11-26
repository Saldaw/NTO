using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;
using UnityEngine.U2D;

public class ShopUI : MonoBehaviour
{
    [SerializeField] private GameObject loadingMenu;
    [SerializeField] private GameObject counterMenu;
    [SerializeField] private TextMeshProUGUI counterMenuMainText;
    [SerializeField] private TextMeshProUGUI counterMenuCountText;
    [SerializeField] private TextMeshProUGUI counterMenuPriceText;
    [SerializeField] private Button counterButtonBuy;
    const string buingText = "Покупка";
    const string sellingText = "Продажа";
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
    [SerializeField] private ShopController shopCR;
    [SerializeField] private Inventory playerInventory;
    [SerializeField] private Dictionary<string, Image> imagesForShops;
    [SerializeField] private List<Sprite> imagesForButton;

    private int resoursInCounter;
    private bool isBuing;
    private int countInCounter;
    private int countResoursInCounter;
    private int countResoursInPlayer;
    private int costInCounter;
    private int summCostInCounter;
    private Shop shopNow;

    public void UpdateInfo(Shop info)
    {
        if (info.resources.food.count > 0) { buttonBuy1.enabled = true; buttonBuy1.image.sprite = imagesForButton[0]; } else { buttonBuy1.enabled = false; buttonBuy1.image.sprite = imagesForButton[1]; }
        if (info.resources.materials.count > 0) { buttonBuy2.enabled = true; buttonBuy2.image.sprite = imagesForButton[0]; } else { buttonBuy2.enabled = false; buttonBuy2.image.sprite = imagesForButton[1]; }
        if (info.resources.electronics.count > 0) { buttonBuy3.enabled = true; buttonBuy3.image.sprite = imagesForButton[0]; } else { buttonBuy3.enabled = false; buttonBuy3.image.sprite = imagesForButton[1]; }
        if (info.resources.weapons.count > 0) { buttonBuy4.enabled = true; buttonBuy4.image.sprite = imagesForButton[0]; } else { buttonBuy4.enabled = false; buttonBuy4.image.sprite = imagesForButton[1]; }
        if (info.resources.energyhoney.count > 0) { buttonBuy5.enabled = true; buttonBuy5.image.sprite = imagesForButton[0]; } else { buttonBuy5.enabled = false; buttonBuy5.image.sprite = imagesForButton[1]; }

        cost1Text.text = info.resources.food.cost.ToString();
        count1Text.text = info.resources.food.count.ToString();
        cost2Text.text = info.resources.materials.cost.ToString();
        count2Text.text = info.resources.materials.count.ToString();
        cost3Text.text = info.resources.electronics.cost.ToString();
        count3Text.text = info.resources.electronics.count.ToString();
        cost4Text.text = info.resources.weapons.cost.ToString();
        count4Text.text = info.resources.weapons.count.ToString();
        cost5Text.text = info.resources.energyhoney.cost.ToString();
        count5Text.text = info.resources.energyhoney.count.ToString();
        shopNow = info;
        loadingMenu.SetActive(false);
    }
    private void Start()
    {

        
    }

    public void Open(string shopName)
    {
        Shop shop = shopCR.GetShop(shopName);
        if (shop != null)
        {
            UpdateInfo(shop);
        }
        else loadingMenu.SetActive(true);
    }
    public void SelectCountResourses(string resoursNameIsBuing)
    {
        switch (resoursNameIsBuing)
        {
            case "1Buy":
                resoursInCounter = 1;
                isBuing = true;
                break;
            case "1Sell":
                resoursInCounter = 1;
                isBuing = false;
                break;
            case "2Buy":
                resoursInCounter = 2;
                isBuing = true;
                break;
            case "2Sell":
                resoursInCounter = 2;
                isBuing = false;
                break;
            case "3Buy":
                resoursInCounter = 3;
                isBuing = true;
                break;
            case "3Sell":
                resoursInCounter = 3;
                isBuing = false;
                break;
            case "4Buy":
                resoursInCounter = 4;
                isBuing = true;
                break;
            case "4Sell":
                resoursInCounter = 4;
                isBuing = false;
                break;
            case "5Buy":
                resoursInCounter = 5;
                isBuing = true;
                break;
            case "5Sell":
                resoursInCounter = 5;
                isBuing = false;
                break;
        }
        switch (resoursInCounter)
        {
            case 1:
                countResoursInCounter = shopNow.resources.food.count;
                costInCounter = shopNow.resources.food.cost;
                countResoursInPlayer = playerInventory.GetLocalInventory().food;
                break;
            case 2:
                countResoursInCounter = shopNow.resources.materials.count;
                costInCounter = shopNow.resources.materials.cost;
                countResoursInPlayer = playerInventory.GetLocalInventory().materials;
                break;
            case 3:
                countResoursInCounter = shopNow.resources.electronics.count;
                costInCounter = shopNow.resources.electronics.cost;
                countResoursInPlayer = playerInventory.GetLocalInventory().electronics;
                break;
            case 4:
                countResoursInCounter = shopNow.resources.weapons.count;
                costInCounter = shopNow.resources.weapons.cost;
                countResoursInPlayer = playerInventory.GetLocalInventory().weapons;
                break;
            case 5:
                countResoursInCounter = shopNow.resources.energyhoney.count;
                costInCounter = shopNow.resources.energyhoney.cost;
                countResoursInPlayer = playerInventory.GetLocalInventory().energyhoney;
                break;
        }
        counterMenuPriceText.text = "";
        countInCounter = 0;
        counterMenuCountText.text = countInCounter.ToString();
        if (isBuing) counterMenuMainText.text = buingText; else counterMenuMainText.text = sellingText;
        counterMenu.SetActive(true);
    }
    public void AddCount(int count)
    {
        if (countInCounter + count > 0)
        {
            countInCounter += count;
            summCostInCounter = countInCounter * costInCounter;
            if (isBuing)
            {
                counterMenuPriceText.text = $"-{summCostInCounter}";
                counterMenuCountText.text = countInCounter.ToString();
                if (playerInventory.GetLocalInventory().goldhoney >= summCostInCounter && countResoursInCounter >= countInCounter)
                {
                    counterButtonBuy.enabled = true;
                }
                else
                {
                    counterButtonBuy.enabled = false;
                }
            }
            else
            {
                counterMenuPriceText.text = $"+{summCostInCounter}";
                counterMenuCountText.text = countInCounter.ToString();
                if (countResoursInPlayer >= countInCounter)
                {
                    counterButtonBuy.enabled = true;
                }
                else
                {
                    counterButtonBuy.enabled = false;
                }
            }
        }
    }
    public void ConfirmBuing()
    {
        if (countInCounter != 0)
        {
            if (!isBuing) countInCounter = -countInCounter;
            ShopResources shopResources = new ShopResources();
            switch (resoursInCounter)
            {
                case 1:
                    shopResources.food.count = countInCounter;
                    shopResources.food.cost = costInCounter;
                    break;
                case 2:
                    shopResources.materials.count = countInCounter;
                    shopResources.materials.cost = costInCounter;
                    break;
                case 3:
                    shopResources.electronics.count = countInCounter;
                    shopResources.electronics.cost = costInCounter;
                    break;
                case 4:
                    shopResources.weapons.count = countInCounter;
                    shopResources.weapons.cost = costInCounter;
                    break;
                case 5:
                    shopResources.energyhoney.count = countInCounter;
                    shopResources.energyhoney.cost = costInCounter;
                    break;
            }
            shopCR.BuyResourse(shopResources, shopNow.name);
        }
        UpdateInfo(shopNow);
        counterMenu.SetActive(false);
    }
}
