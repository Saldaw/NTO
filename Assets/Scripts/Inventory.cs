using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Unity.VisualScripting;
using UnityEngine;
using Newtonsoft.Json;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    //----------------------Название в логах---------------------\\
    const string add_food = "add_food";
    const string add_materials = "add_materials";
    const string add_electronics = "add_electronics";
    const string add_weapons = "add_weapons";
    const string add_energyhoney = "add_energyhoney";
    const string add_goldhoney = "add_goldhoney";
    //-----------------------------------------------------------\\
    private string playerName;
    private bool online;
    //--------------------------Классы--------------------------\\
    [Serializable] public class PlayerInventory : ICloneable
    {
        public int food = 0;
        public int materials = 0;
        public int electronics = 0;
        public int weapons = 0;
        public int energyhoney = 0;
        public int goldhoney = 0;

        public object Clone()
        {
            return MemberwiseClone();
        }
    } //Класс инвентаря игрока
    [Serializable] private class AnswerServer//Класс ответа сервера 
    {
        public string name;
        public PlayerInventory resources;
    }
    [Serializable] private class Log//Класс лога 
    {
        public string comment;
        public string player_name;
        public Dictionary<string,int> resources_changed;
    }
    //-----------------------------------------------------------\\
    private List<Log> Logs = new List<Log>();
    [SerializeField] private ConnectionErrorUI errorUI;
    [SerializeField] private ResursInfo resursInfo;
    //--------------------------Инвентарь--------------------------\\
    private PlayerInventory localPlayerInventory = new PlayerInventory
    {
        food = 0,
        materials = 0,
        electronics = 0,
        weapons = 0,
        energyhoney = 1,
        goldhoney = 0,
    };
    private PlayerInventory changesPlayerInventory = new PlayerInventory
    {
        food = 0,
        materials = 0,
        electronics = 0,
        weapons = 0,
        energyhoney = 0,
        goldhoney = 0,
    };
    //-----------------------------------------------------------\\
    
    public void ChangeResurs(PlayerInventory changes, string comment)//Изменить колличество ресурсов 
    {
        Dictionary<string, int> resourcesChanged = new Dictionary<string, int>();

        localPlayerInventory.food += changes.food;
        localPlayerInventory.materials += changes.materials;
        localPlayerInventory.electronics += changes.electronics;
        localPlayerInventory.weapons += changes.weapons;
        localPlayerInventory.energyhoney += changes.energyhoney;
        localPlayerInventory.goldhoney += changes.goldhoney;
        resursInfo.UodateInfo(localPlayerInventory);
        if (online)
        {
            changesPlayerInventory.food += changes.food;
            changesPlayerInventory.materials += changes.materials;
            changesPlayerInventory.electronics += changes.electronics;
            changesPlayerInventory.weapons += changes.weapons;
            changesPlayerInventory.energyhoney += changes.energyhoney;
            changesPlayerInventory.goldhoney += changes.goldhoney;
            if (changes.food != 0) resourcesChanged.Add(add_food, changes.food);
            if (changes.materials != 0) resourcesChanged.Add(add_materials, changes.materials);
            if (changes.electronics != 0) resourcesChanged.Add(add_electronics, changes.electronics);
            if (changes.weapons != 0) resourcesChanged.Add(add_weapons, changes.weapons);
            if (changes.energyhoney != 0) resourcesChanged.Add(add_energyhoney, changes.energyhoney);
            if (changes.goldhoney != 0) resourcesChanged.Add(add_goldhoney, changes.goldhoney);
            CreateLog(comment, resourcesChanged);
        }
    }
    public PlayerInventory GetLocalInventory()
    {
        return localPlayerInventory;
    } //Получить локальный инвентарь

    private void CreateLog(string comment, Dictionary<string, int> resourcesThatChanged)//Создание лога 
    {
        Log log = new Log()
        {
            comment = $"{DateTime.UtcNow}: {comment}",
            player_name = playerName,
            resources_changed = resourcesThatChanged
        };
        Logs.Add(log);
    }
    private void CheckCorrect(PlayerInventory inventoryOnServer)//Проверка совпадения локальных данных и данных сервера 
    {
        PlayerInventory correctPlayerInventory = new PlayerInventory()
        {
            food = inventoryOnServer.food + changesPlayerInventory.food,
            materials = inventoryOnServer.materials + changesPlayerInventory.materials,
            electronics = inventoryOnServer.electronics + changesPlayerInventory.electronics,
            weapons = inventoryOnServer.weapons + changesPlayerInventory.weapons,
            energyhoney = inventoryOnServer.energyhoney + changesPlayerInventory.energyhoney,
            goldhoney = inventoryOnServer.goldhoney + changesPlayerInventory.goldhoney,
        };//Получение корректных значений
        changesPlayerInventory = new PlayerInventory()
        {
            food = 0,
            materials = 0,
            electronics = 0,
            weapons = 0,
            energyhoney = 0,
            goldhoney = 0,
        };//Удаление изменений
        string s1 = JsonUtility.ToJson(correctPlayerInventory);
        string s2 = JsonUtility.ToJson(localPlayerInventory);
        if (s1 != s2)
        {
            Debug.Log("The value on the local machine is different from the server value!");
            localPlayerInventory = (PlayerInventory)correctPlayerInventory.Clone();
            resursInfo.UodateInfo(localPlayerInventory);
        }
        SetInventoryOnServer(this, playerName, correctPlayerInventory);
    }

    //---------------------Работа с сервером---------------------\\
    static async void GetInventoryFromServer(Inventory self)//Получение инвентаря игрока с сервера 
    {
        try
        {
            using var httpClient = new HttpClient();
            string url = $"https://2025.nti-gamedev.ru/api/games/e9631fb2-0408-421c-a35f-140a70f2a916/players/{self.playerName}";
            HttpResponseMessage response = await httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                string jsonString = await response.Content.ReadAsStringAsync();
                AnswerServer answerServer = JsonUtility.FromJson<AnswerServer>(jsonString);
                self.CheckCorrect(answerServer.resources);
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
    static async void SetInventoryOnServer(Inventory self, string playerName, PlayerInventory newInventory)//Обновление инвентаря игрока на сервере 
    {
        string requestUrl = $"https://2025.nti-gamedev.ru/api/games/e9631fb2-0408-421c-a35f-140a70f2a916/players/{playerName}/";

        HttpClient httpClient = new HttpClient();
        AnswerServer resurses = new AnswerServer() {resources = newInventory};
        string inventoryJson = JsonUtility.ToJson(resurses);
        var content = new StringContent(inventoryJson, System.Text.Encoding.UTF8, "application/json");

        try
        {
            var response = await httpClient.PutAsync(requestUrl, content);

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
    static async void PostLogOnServer(Inventory self, Log log)//Отправка лога на сервер 
    {
        string requestUrl = $"https://2025.nti-gamedev.ru/api/games/e9631fb2-0408-421c-a35f-140a70f2a916/logs/";

        HttpClient httpClient = new HttpClient();
        string logJson = JsonConvert.SerializeObject(log);
        var content = new StringContent(logJson, System.Text.Encoding.UTF8, "application/json");
        try
        {
            var response = await httpClient.PostAsync(requestUrl, content);

            if (!response.IsSuccessStatusCode)
            {
                self.errorUI.AddError(content, requestUrl);
            }
        }
        catch (Exception ex)
        {
            self.errorUI.AddError(content, requestUrl);
            Debug.Log($"An error occurred: {ex.Message}");
        }
    }
    //-----------------------------------------------------------\\

    void Start()
    {
        playerName = PlayerPrefs.GetString("Name");
        online = PlayerPrefs.GetInt("Online") == 1;
        GetInventoryFromServer(this);
        StartCoroutine(Synchronization());
    }
    private IEnumerator Synchronization()//Синхронизация с сервером 
    {
        while (true)
        {
            if (Logs.Count > 0)
            {
                GetInventoryFromServer(this);
                for (int i = 0; i < Logs.Count; i++)
                {
                    PostLogOnServer(this, Logs[i]);
                }
                Logs.Clear();
            }
            yield return new WaitForSeconds(5f);
        }
    }
}
