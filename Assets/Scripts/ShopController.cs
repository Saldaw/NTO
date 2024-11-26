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
    const string buying_food = "buying_food";
    const string buying_materials = "buying_materials";
    const string buying_electronics = "buying_electronics";
    const string buying_weapons = "buying_weapons";
    const string buying_energyhoney = "buying_energyhoney";
    const string selling_food = "selling_food";
    const string selling_materials = "selling_materials";
    const string selling_electronics = "selling_electronics";
    const string selling_weapons = "selling_weapons";
    const string selling_energyhoney = "selling_energyhoney";
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
    readonly Shop localShop1 = new Shop()
    {
        name = "City1Shop",
        resources = new ShopResources()
        {
            food = { count = 5, cost = 10 },
            electronics = { count = 2, cost = 20 },
            energyhoney = { count = 15, cost = 10 },
            materials = { count = 1, cost = 20 },
            weapons = { count = 0, cost = 10 }
        }
    }; //Локальный магазин 1
    readonly Shop localShop2 = new Shop()
    {
        name = "City2Shop",
        resources = new ShopResources()
        {
            food = { count = 2, cost = 15 },
            electronics = { count = 4, cost = 15 },
            energyhoney = { count = 15, cost = 10 },
            materials = { count = 10, cost = 10 },
            weapons = { count = 0, cost = 10 }
        }
    }; //Локальный магазин 2
    readonly Shop localShop3 = new Shop()
    {
        name = "City3Shop",
        resources = new ShopResources()
        {
            food = { count = 15, cost = 12 },
            electronics = { count = 0, cost = 20 },
            energyhoney = { count = 3, cost = 15 },
            materials = { count = 3, cost = 15 },
            weapons = { count = 0, cost = 10 }
        }
    }; //Локальный магазин 3
    readonly Shop localShop4 = new Shop()
    {
        name = "City4Shop",
        resources = new ShopResources()
        {
            food = { count = 5, cost = 10 },
            electronics = { count = 5, cost = 10 },
            energyhoney = { count = 5, cost = 10 },
            materials = { count = 5, cost = 10 },
            weapons = { count = 5, cost = 10 }
        }
    }; //Локальный магазин 4
    readonly Shop localShop6 = new Shop()
    {
        name = "City6Shop",
        resources = new ShopResources()
        {
            food = { count = 0, cost = 20 },
            electronics = { count = 2, cost = 15 },
            energyhoney = { count = 0, cost = 5 },
            materials = { count = 15, cost = 7 },
            weapons = { count = 5, cost = 10 }
        }
    }; //Локальный магазин 6

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
                case "City1Shop":
                    shopFromServer = localShop1;
                    return localShop1;
                case "City2Shop":
                    shopFromServer = localShop2;
                    return localShop2;
                case "City3Shop":
                    shopFromServer = localShop3;
                    return localShop3;
                case "City4Shop":
                    shopFromServer = localShop4;
                    return localShop4;
                case "City6Shop":
                    shopFromServer = localShop6;
                    return localShop6;
                default: return null;

            }
        }

    }
    public void BuyResourse(ShopResources buyingResonrces, string shopName)//Взаимодействие с магазином 
    {
        Inventory.PlayerInventory inventory = playerInventory.GetLocalInventory();

        if (buyingResonrces.food.count>0 && inventory.goldhoney - buyingResonrces.food.count * buyingResonrces.food.cost >= 0 && shopFromServer.resources.food.count>= buyingResonrces.food.count)
        {
            playerInventory.ChangeResurs(new Inventory.PlayerInventory() { goldhoney = - buyingResonrces.food.count * buyingResonrces.food.cost, food = buyingResonrces.food.count }, buying_food);
            ChangeShopResurs(shopName, new ShopResources() { food = buyingResonrces.food });
        }
        else if (buyingResonrces.materials.count > 0 && inventory.goldhoney - buyingResonrces.materials.count * buyingResonrces.materials.cost >= 0 && shopFromServer.resources.materials.count >= buyingResonrces.materials.count)
        {
            playerInventory.ChangeResurs(new Inventory.PlayerInventory() { goldhoney = - buyingResonrces.materials.count * buyingResonrces.materials.cost, materials = buyingResonrces.materials.count }, buying_materials);
            ChangeShopResurs(shopName, new ShopResources() { materials = buyingResonrces.materials });
        }
        else if(buyingResonrces.electronics.count > 0 && inventory.goldhoney - buyingResonrces.electronics.count * buyingResonrces.electronics.cost >= 0 && shopFromServer.resources.electronics.count >= buyingResonrces.electronics.count)
        {
            playerInventory.ChangeResurs(new Inventory.PlayerInventory() { goldhoney = - buyingResonrces.electronics.count * buyingResonrces.electronics.cost, electronics = buyingResonrces.electronics.count }, buying_electronics);
            ChangeShopResurs(shopName, new ShopResources() { electronics = buyingResonrces.electronics });
        }
        else if (buyingResonrces.weapons.count > 0 && inventory.goldhoney - buyingResonrces.weapons.count * buyingResonrces.weapons.cost >= 0 && shopFromServer.resources.weapons.count >= buyingResonrces.weapons.count)
        {
            playerInventory.ChangeResurs(new Inventory.PlayerInventory() { goldhoney = -buyingResonrces.weapons.count * buyingResonrces.weapons.cost, weapons = buyingResonrces.weapons.count }, buying_weapons);
            ChangeShopResurs(shopName, new ShopResources() { weapons = buyingResonrces.weapons });
        }
        else if (buyingResonrces.energyhoney.count > 0 && inventory.goldhoney - buyingResonrces.energyhoney.count * buyingResonrces.energyhoney.cost >= 0 && shopFromServer.resources.energyhoney.count >= buyingResonrces.energyhoney.count)
        {
            playerInventory.ChangeResurs(new Inventory.PlayerInventory() { goldhoney = -buyingResonrces.energyhoney.count * buyingResonrces.energyhoney.cost, energyhoney = buyingResonrces.energyhoney.count }, buying_energyhoney);
            ChangeShopResurs(shopName, new ShopResources() { energyhoney = buyingResonrces.energyhoney });
        }

        else if(buyingResonrces.food.count < 0 && inventory.food >= -buyingResonrces.food.count)
        {
            playerInventory.ChangeResurs(new Inventory.PlayerInventory() { goldhoney = buyingResonrces.food.count * -buyingResonrces.food.cost, food = buyingResonrces.food.count }, selling_food);
            ChangeShopResurs(shopName, new ShopResources() { food = buyingResonrces.food });
        }
        else if (buyingResonrces.materials.count < 0 && inventory.materials >= -buyingResonrces.materials.count)
        {
            playerInventory.ChangeResurs(new Inventory.PlayerInventory() { goldhoney = buyingResonrces.materials.count * -buyingResonrces.materials.cost, materials = buyingResonrces.materials.count }, selling_materials);
            ChangeShopResurs(shopName, new ShopResources() { materials = buyingResonrces.materials });
        }
        else if (buyingResonrces.electronics.count < 0 && inventory.electronics >= -buyingResonrces.electronics.count)
        {
            playerInventory.ChangeResurs(new Inventory.PlayerInventory() { goldhoney = buyingResonrces.electronics.count * -buyingResonrces.electronics.cost, electronics = buyingResonrces.electronics.count }, selling_electronics);
            ChangeShopResurs(shopName, new ShopResources() { electronics = buyingResonrces.electronics });
        }
        else if (buyingResonrces.weapons.count < 0 && inventory.weapons >= -buyingResonrces.weapons.count)
        {
            playerInventory.ChangeResurs(new Inventory.PlayerInventory() { goldhoney = buyingResonrces.weapons.count * -buyingResonrces.weapons.cost, weapons = buyingResonrces.weapons.count }, selling_weapons);
            ChangeShopResurs(shopName, new ShopResources() { weapons = buyingResonrces.weapons });
        }
        else if (buyingResonrces.energyhoney.count < 0 && inventory.energyhoney >= -buyingResonrces.energyhoney.count)
        {
            playerInventory.ChangeResurs(new Inventory.PlayerInventory() { goldhoney = buyingResonrces.energyhoney.count * -buyingResonrces.energyhoney.cost, energyhoney = buyingResonrces.energyhoney.count }, selling_energyhoney);
            ChangeShopResurs(shopName, new ShopResources() { energyhoney = buyingResonrces.energyhoney });
        }
    }
    private void ChangeShopResurs(string shopName, ShopResources changeResurs)//Изменение ресурсов в магазине 
    {
        if (online)
        {
            Dictionary<string, int> resourcesChanged = new Dictionary<string, int>();
            shopFromServer.resources.food.count -= changeResurs.food.count;
            shopFromServer.resources.materials.count -= changeResurs.materials.count;
            shopFromServer.resources.electronics.count -= changeResurs.electronics.count;
            shopFromServer.resources.weapons.count -= changeResurs.weapons.count;
            shopFromServer.resources.energyhoney.count -= changeResurs.energyhoney.count;

            if (changeResurs.food.count != 0) resourcesChanged.Add(buying_food, changeResurs.food.count);
            if (changeResurs.materials.count != 0) resourcesChanged.Add(buying_materials, changeResurs.materials.count);
            if (changeResurs.electronics.count != 0) resourcesChanged.Add(buying_electronics, changeResurs.electronics.count);
            if (changeResurs.weapons.count != 0) resourcesChanged.Add(buying_weapons, changeResurs.weapons.count);
            if (changeResurs.energyhoney.count != 0) resourcesChanged.Add(buying_energyhoney, changeResurs.energyhoney.count);
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
