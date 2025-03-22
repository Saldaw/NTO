using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using UnityEngine.UI;

    //--------------------------Классы--------------------------\\
    [Serializable] public class Shop//Класс магазина 
    {
        public string name;
        public ShopResources resources = new ShopResources();

    }
    [Serializable] public class ShopResources//Класс с ресурсами 
    {
        public ResourceInShop food = new ResourceInShop();
        public ResourceInShop materials = new ResourceInShop();
        public ResourceInShop electronics = new ResourceInShop();
        public ResourceInShop weapons = new ResourceInShop();
        public ResourceInShop energyhoney = new ResourceInShop();
    }
    [Serializable] public class ResourceInShop//Класс ресурса 
    {
        public int count = 0;
        public int cost = 0;
    }
    //-----------------------------------------------------------\\

public class ShopCreator : MonoBehaviour
{

    private string playerName;
    private void Awake()//Создаём магазин при инициализации
    {
        playerName = PlayerPrefs.GetString("Name");
        ShopResources shopResurses = new ShopResources() { food = {count = 5, cost = 2 }, electronics = {count = 2, cost = 20 }, energyhoney = {count = 15, cost = 10}, materials = {count = 1, cost = 20 }, weapons = {count = 0, cost = 10}};
        CreateShop("City1Shop", shopResurses);
        shopResurses = new ShopResources() { food = { count = 2, cost = 2 }, electronics = { count = 4, cost = 15 }, energyhoney = { count = 15, cost = 10 }, materials = { count = 10, cost = 10 }, weapons = { count = 0, cost = 10 } };
        CreateShop("City2Shop", shopResurses);
        shopResurses = new ShopResources() { food = { count = 15, cost = 2 }, electronics = { count = 0, cost = 20 }, energyhoney = { count = 3, cost = 15 }, materials = { count = 3, cost = 15 }, weapons = { count = 0, cost = 10 } };
        CreateShop("City3Shop", shopResurses);
        shopResurses = new ShopResources() { food = { count = 5, cost = 10 }, electronics = { count = 5, cost = 10 }, energyhoney = { count = 5, cost = 10 }, materials = { count = 5, cost = 10 }, weapons = { count = 5, cost = 10 } };
        CreateShop("City4Shop", shopResurses);
        shopResurses = new ShopResources() { food = { count = 0, cost = 5 }, electronics = { count = 2, cost = 15 }, energyhoney = { count = 0, cost = 5 }, materials = { count = 15, cost = 7 }, weapons = { count = 5, cost = 10 } };
        CreateShop("City6Shop", shopResurses);
    }

    //---------------------Создание Магазина---------------------\\
    private void CreateShop(string name, ShopResources shopRes)//Создать магазин 
    {
        Shop shop = new Shop();
        shop.name = name;
        shop.resources = shopRes;
        PostShopOnServer(playerName, shop);
    }
    static async void PostShopOnServer(string playerName, Shop shop)//Создать магазин на сервере 
    {
        string requestUrl = $"https://2025.nti-gamedev.ru/api/games/e9631fb2-0408-421c-a35f-140a70f2a916/players/{playerName}/shops/";

        HttpClient httpClient = new HttpClient();
        string inventoryJson = JsonUtility.ToJson(shop);
        var content = new StringContent(inventoryJson, System.Text.Encoding.UTF8, "application/json");

        try
        {
            var response = await httpClient.PostAsync(requestUrl, content);

            if (!response.IsSuccessStatusCode)
            {
                Debug.Log("Synchronization error");
            }
        }
        catch (Exception ex)
        {
            Debug.Log($"An error occurred: {ex.Message}");
        }
    }
    //-----------------------------------------------------------\\
}
