using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using UnityEditor.PackageManager;
using UnityEditor.VersionControl;
using UnityEngine;

public class ShopController : MonoBehaviour
{
    //----------------------Название в логах---------------------\\
    const string buying_resurs1 = "buying_resurs1";
    const string buying_resurs2 = "buying_resurs2";
    const string buying_resurs3 = "buying_resurs3";
    const string buying_resurs4 = "buying_resurs4";
    const string buying_resurs5 = "buying_resurs5";
    const string selling_resurs1 = "selling_resurs1";
    const string selling_resurs2 = "selling_resurs2";
    const string selling_resurs3 = "selling_resurs3";
    const string selling_resurs4 = "selling_resurs4";
    const string selling_resurs5 = "selling_resurs5";
    //-----------------------------------------------------------\\

    private string playerName;
    private bool online;

    [Serializable] private class Log//Класс лога 
    {
        public string comment;
        public string player_name;
        public string shop_name;
        public Dictionary<string,int> resources_changed;
    }
    private Shop shopFromServer;
    readonly Shop localShop = new Shop()
    {
        name = "Магаз",
        resources = new ShopResources()
        {
            Resurse1 = new ResourceInShop()
            {
                cost = 10,
                count = 10
            },
            Resurse2 = new ResourceInShop()
            {
                cost = 10,
                count = 10
            },
            Resurse3 = new ResourceInShop()
            {
                cost = 10,
                count = 10
            },
            Resurse4 = new ResourceInShop()
            {
                cost = 10,
                count = 10
            },
            Resurse5 = new ResourceInShop()
            {
                cost = 10,
                count = 10
            }
        }
    }; //Локальный магазин 1

    //---------------------------Ссылки--------------------------\\
    [SerializeField] private ShopUI shopUI;
    [SerializeField] private Inventory playerInventory;
    [SerializeField] private ConnectionErrorUI errorUI;
    //-----------------------------------------------------------\\
    
    void Start()
    {
        

    }

    public Shop GetShop(string shopName)//Получение магазина 
    {
        playerName = PlayerPrefs.GetString("Name");
        online = PlayerPrefs.GetInt("Online") == 1;
        if (online)
        {
            GetShopFromServer(this, shopName);
            return null;
        }
        else
        {
            switch (shopName)
            {
                case "1":
                    shopFromServer = localShop;
                    return localShop;
                default: return null;
            }
        }

    }
    public void BuyResourse(ShopResources buyingResonrces, string shopName)//Взаимодействие с магазином 
    {
        Inventory.PlayerInventory inventory = playerInventory.GetLocalInventory();

        if (buyingResonrces.Resurse1.count>0 && inventory.gold - buyingResonrces.Resurse1.count * buyingResonrces.Resurse1.cost >= 0 && shopFromServer.resources.Resurse1.count>= buyingResonrces.Resurse1.count)
        {
            playerInventory.ChangeResurs(new Inventory.PlayerInventory() { gold = - buyingResonrces.Resurse1.count * buyingResonrces.Resurse1.cost, resurs1 = buyingResonrces.Resurse1.count }, buying_resurs1);
            ChangeShopResurs(shopName, new ShopResources() { Resurse1 = buyingResonrces.Resurse1 });
        }
        else if (buyingResonrces.Resurse2.count > 0 && inventory.gold - buyingResonrces.Resurse2.count * buyingResonrces.Resurse2.cost >= 0 && shopFromServer.resources.Resurse2.count >= buyingResonrces.Resurse2.count)
        {
            playerInventory.ChangeResurs(new Inventory.PlayerInventory() { gold = - buyingResonrces.Resurse2.count * buyingResonrces.Resurse2.cost, resurs2 = buyingResonrces.Resurse2.count }, buying_resurs2);
            ChangeShopResurs(shopName, new ShopResources() { Resurse2 = buyingResonrces.Resurse2 });
        }
        else if(buyingResonrces.Resurse3.count > 0 && inventory.gold - buyingResonrces.Resurse3.count * buyingResonrces.Resurse3.cost >= 0 && shopFromServer.resources.Resurse3.count >= buyingResonrces.Resurse3.count)
        {
            playerInventory.ChangeResurs(new Inventory.PlayerInventory() { gold = - buyingResonrces.Resurse3.count * buyingResonrces.Resurse3.cost, resurs3 = buyingResonrces.Resurse3.count }, buying_resurs3);
            ChangeShopResurs(shopName, new ShopResources() { Resurse3 = buyingResonrces.Resurse3 });
        }
        else if (buyingResonrces.Resurse4.count > 0 && inventory.gold - buyingResonrces.Resurse4.count * buyingResonrces.Resurse4.cost >= 0 && shopFromServer.resources.Resurse4.count >= buyingResonrces.Resurse4.count)
        {
            playerInventory.ChangeResurs(new Inventory.PlayerInventory() { gold = -buyingResonrces.Resurse4.count * buyingResonrces.Resurse4.cost, resurs4 = buyingResonrces.Resurse4.count }, buying_resurs4);
            ChangeShopResurs(shopName, new ShopResources() { Resurse4 = buyingResonrces.Resurse4 });
        }
        else if (buyingResonrces.Resurse5.count > 0 && inventory.gold - buyingResonrces.Resurse5.count * buyingResonrces.Resurse5.cost >= 0 && shopFromServer.resources.Resurse5.count >= buyingResonrces.Resurse5.count)
        {
            playerInventory.ChangeResurs(new Inventory.PlayerInventory() { gold = -buyingResonrces.Resurse5.count * buyingResonrces.Resurse5.cost, resurs5 = buyingResonrces.Resurse5.count }, buying_resurs5);
            ChangeShopResurs(shopName, new ShopResources() { Resurse5 = buyingResonrces.Resurse5 });
        }

        else if(buyingResonrces.Resurse1.count < 0 && inventory.resurs1 >= -buyingResonrces.Resurse1.count)
        {
            playerInventory.ChangeResurs(new Inventory.PlayerInventory() { gold = buyingResonrces.Resurse1.count * -buyingResonrces.Resurse1.cost, resurs1 = buyingResonrces.Resurse1.count }, selling_resurs1);
            ChangeShopResurs(shopName, new ShopResources() { Resurse1 = buyingResonrces.Resurse1 });
        }
        else if (buyingResonrces.Resurse2.count < 0 && inventory.resurs2 >= -buyingResonrces.Resurse2.count)
        {
            playerInventory.ChangeResurs(new Inventory.PlayerInventory() { gold = buyingResonrces.Resurse2.count * -buyingResonrces.Resurse2.cost, resurs2 = buyingResonrces.Resurse2.count }, selling_resurs2);
            ChangeShopResurs(shopName, new ShopResources() { Resurse2 = buyingResonrces.Resurse2 });
        }
        else if (buyingResonrces.Resurse3.count < 0 && inventory.resurs3 >= -buyingResonrces.Resurse3.count)
        {
            playerInventory.ChangeResurs(new Inventory.PlayerInventory() { gold = buyingResonrces.Resurse3.count * -buyingResonrces.Resurse3.cost, resurs3 = buyingResonrces.Resurse3.count }, selling_resurs3);
            ChangeShopResurs(shopName, new ShopResources() { Resurse3 = buyingResonrces.Resurse3 });
        }
        else if (buyingResonrces.Resurse4.count < 0 && inventory.resurs4 >= -buyingResonrces.Resurse4.count)
        {
            playerInventory.ChangeResurs(new Inventory.PlayerInventory() { gold = buyingResonrces.Resurse4.count * -buyingResonrces.Resurse4.cost, resurs4 = buyingResonrces.Resurse4.count }, selling_resurs4);
            ChangeShopResurs(shopName, new ShopResources() { Resurse4 = buyingResonrces.Resurse4 });
        }
        else if (buyingResonrces.Resurse5.count < 0 && inventory.resurs5 >= -buyingResonrces.Resurse5.count)
        {
            playerInventory.ChangeResurs(new Inventory.PlayerInventory() { gold = buyingResonrces.Resurse5.count * -buyingResonrces.Resurse5.cost, resurs5 = buyingResonrces.Resurse5.count }, selling_resurs5);
            ChangeShopResurs(shopName, new ShopResources() { Resurse5 = buyingResonrces.Resurse5 });
        }
    }
    private void ChangeShopResurs(string shopName, ShopResources changeResurs)//Изменение ресурсов в магазине 
    {
        if (online)
        {
            Dictionary<string, int> resourcesChanged = new Dictionary<string, int>();
            shopFromServer.resources.Resurse1.count -= changeResurs.Resurse1.count;
            shopFromServer.resources.Resurse2.count -= changeResurs.Resurse2.count;
            shopFromServer.resources.Resurse3.count -= changeResurs.Resurse3.count;
            shopFromServer.resources.Resurse4.count -= changeResurs.Resurse4.count;
            shopFromServer.resources.Resurse5.count -= changeResurs.Resurse5.count;

            if (changeResurs.Resurse1.count != 0) resourcesChanged.Add(buying_resurs1, changeResurs.Resurse1.count);
            if (changeResurs.Resurse2.count != 0) resourcesChanged.Add(buying_resurs2, changeResurs.Resurse2.count);
            if (changeResurs.Resurse3.count != 0) resourcesChanged.Add(buying_resurs3, changeResurs.Resurse3.count);
            if (changeResurs.Resurse4.count != 0) resourcesChanged.Add(buying_resurs4, changeResurs.Resurse4.count);
            if (changeResurs.Resurse5.count != 0) resourcesChanged.Add(buying_resurs5, changeResurs.Resurse5.count);
            SetShopOnServer(this, shopName);
            CreateLog("The player sold resources", shopName, resourcesChanged);
        }
    }

    private void CreateLog(string comment, string shopName, Dictionary<string, int> resourcesThatChanged)//Создание лога 
    {
        Log log = new Log()
        {
            comment = $"{DateTime.UtcNow}: {comment}",
            player_name = playerName,
            shop_name = shopName,
            resources_changed = resourcesThatChanged
        };
        PostLogOnServer(this,log);
    }

    //---------------------Работа с сервером---------------------\\
    static async void GetShopFromServer(ShopController self, string shopName)//Получение магазина с сервера 
    {
        try
        {
            using var httpClient = new HttpClient();
            string url = $"https://2025.nti-gamedev.ru/api/games/d5ebfca3-ee6d-485f-9a9b-a53809bfcb62/players/{self.playerName}/shops/{shopName}/";
            HttpResponseMessage response = await httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                string jsonString = await response.Content.ReadAsStringAsync();
                self.shopFromServer = JsonUtility.FromJson<Shop>(jsonString);
                self.shopUI.UpdateInfo(self.shopFromServer);
            }
            else
            {
                Debug.Log("Error while receiving data.");
            }
        }
        catch (Exception ex)
        {
            Debug.Log($"An error occurred: {ex.Message}");
        }
    }
    static async void SetShopOnServer(ShopController self, string shopName)//Изменение магазина на сервере 
    {
        string url = $"https://2025.nti-gamedev.ru/api/games/d5ebfca3-ee6d-485f-9a9b-a53809bfcb62/players/{self.playerName}/shops/{shopName}/";

        HttpClient httpClient = new HttpClient();
        string inventoryJson = JsonUtility.ToJson(self.shopFromServer);
        var content = new StringContent(inventoryJson, System.Text.Encoding.UTF8, "application/json");

        try
        {
            var response = await httpClient.PutAsync(url, content);

            if (!response.IsSuccessStatusCode)
            {
                self.errorUI.AddError(content, url);
                Debug.Log("Error while receiving data.");
            }
        }
        catch (Exception ex)
        {
            self.errorUI.AddError(content, url);
            Debug.Log($"An error occurred: {ex.Message}");
        }
    }
    static async void PostLogOnServer(ShopController self, Log log)//Отправка лога на сервер 
    {
        string requestUrl = $"https://2025.nti-gamedev.ru/api/games/d5ebfca3-ee6d-485f-9a9b-a53809bfcb62/logs/";

        HttpClient httpClient = new HttpClient();
        string logJson = JsonConvert.SerializeObject(log);
        Debug.Log(logJson);
        var content = new StringContent(logJson, System.Text.Encoding.UTF8, "application/json");

        try
        {
            var response = await httpClient.PostAsync(requestUrl, content);

            if (!response.IsSuccessStatusCode)
            {
                self.errorUI.AddError(content, requestUrl);
                Debug.Log("Synchronization error");
            }
        }
        catch (Exception ex)
        {
            self.errorUI.AddError(content, requestUrl);
            Debug.Log($"An error occurred: {ex.Message}");
        }
    }
    //-----------------------------------------------------------\\
}
