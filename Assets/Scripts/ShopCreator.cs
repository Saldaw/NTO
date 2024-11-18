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
        public ResourceInShop Resurse1 = new ResourceInShop();
        public ResourceInShop Resurse2 = new ResourceInShop();
        public ResourceInShop Resurse3 = new ResourceInShop();
        public ResourceInShop Resurse4 = new ResourceInShop();
        public ResourceInShop Resurse5 = new ResourceInShop();
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
        CreateShop("testShop");
    }

    //---------------------Создание Магазина---------------------\\
    private void CreateShop(string name)//Создать магазин 
    {
        Shop shop = new Shop();
        shop.name = name;
        ShopResources resources = new ShopResources();
        resources.Resurse1 = new ResourceInShop() { cost = 10, count = 100 };
        resources.Resurse2 = new ResourceInShop() { cost = 10, count = 100 };
        resources.Resurse3 = new ResourceInShop() { cost = 10, count = 100 };
        resources.Resurse4 = new ResourceInShop() { cost = 10, count = 100 };
        resources.Resurse5 = new ResourceInShop() { cost = 10, count = 100 };
        shop.resources = resources;
        PostShopOnServer(playerName, shop);
    }
    static async void PostShopOnServer(string playerName, Shop shop)//Создать магазин на сервере 
    {
        string requestUrl = $"https://2025.nti-gamedev.ru/api/games/d5ebfca3-ee6d-485f-9a9b-a53809bfcb62/players/{playerName}/shops/";

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
