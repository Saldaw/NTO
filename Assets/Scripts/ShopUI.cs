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
        if (info.resources.Resurse1.count > 0) { buttonBuy1.enabled = true; buttonBuy1.image.sprite = imagesForButton[0]; } else { buttonBuy1.enabled = false; buttonBuy1.image.sprite = imagesForButton[1]; }
        if (info.resources.Resurse2.count > 0) { buttonBuy2.enabled = true; buttonBuy2.image.sprite = imagesForButton[0]; } else { buttonBuy2.enabled = false; buttonBuy2.image.sprite = imagesForButton[1]; }
        if (info.resources.Resurse3.count > 0) { buttonBuy3.enabled = true; buttonBuy3.image.sprite = imagesForButton[0]; } else { buttonBuy3.enabled = false; buttonBuy3.image.sprite = imagesForButton[1]; }
        if (info.resources.Resurse4.count > 0) { buttonBuy4.enabled = true; buttonBuy4.image.sprite = imagesForButton[0]; } else { buttonBuy4.enabled = false; buttonBuy4.image.sprite = imagesForButton[1]; }
        if (info.resources.Resurse5.count > 0) { buttonBuy5.enabled = true; buttonBuy5.image.sprite = imagesForButton[0]; } else { buttonBuy5.enabled = false; buttonBuy5.image.sprite = imagesForButton[1]; }

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
                countResoursInCounter = shopNow.resources.Resurse1.count;
                costInCounter = shopNow.resources.Resurse1.cost;
                countResoursInPlayer = playerInventory.GetLocalInventory().resurs1;
                break;
            case 2:
                countResoursInCounter = shopNow.resources.Resurse2.count;
                costInCounter = shopNow.resources.Resurse2.cost;
                countResoursInPlayer = playerInventory.GetLocalInventory().resurs2;
                break;
            case 3:
                countResoursInCounter = shopNow.resources.Resurse3.count;
                costInCounter = shopNow.resources.Resurse3.cost;
                countResoursInPlayer = playerInventory.GetLocalInventory().resurs3;
                break;
            case 4:
                countResoursInCounter = shopNow.resources.Resurse4.count;
                costInCounter = shopNow.resources.Resurse4.cost;
                countResoursInPlayer = playerInventory.GetLocalInventory().resurs4;
                break;
            case 5:
                countResoursInCounter = shopNow.resources.Resurse5.count;
                costInCounter = shopNow.resources.Resurse5.cost;
                countResoursInPlayer = playerInventory.GetLocalInventory().resurs5;
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
                if (playerInventory.GetLocalInventory().gold >= summCostInCounter && countResoursInCounter >= countInCounter)
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
                    shopResources.Resurse1.count = countInCounter;
                    shopResources.Resurse1.cost = costInCounter;
                    break;
                case 2:
                    shopResources.Resurse2.count = countInCounter;
                    shopResources.Resurse2.cost = costInCounter;
                    break;
                case 3:
                    shopResources.Resurse3.count = countInCounter;
                    shopResources.Resurse3.cost = costInCounter;
                    break;
                case 4:
                    shopResources.Resurse4.count = countInCounter;
                    shopResources.Resurse4.cost = costInCounter;
                    break;
                case 5:
                    shopResources.Resurse5.count = countInCounter;
                    shopResources.Resurse5.cost = costInCounter;
                    break;
            }
            shopCR.BuyResourse(shopResources, shopNow.name);
        }
        UpdateInfo(shopNow);
        counterMenu.SetActive(false);
    }
}
