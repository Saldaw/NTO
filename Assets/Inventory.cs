using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Unity.VisualScripting;
using UnityEngine;
using Newtonsoft.Json;

public class Inventory : MonoBehaviour
{
    //----------------------Название в логах---------------------\\
    const string add_resurs1 = "add_resurs1";
    const string add_resurs2 = "add_resurs2";
    const string add_resurs3 = "add_resurs3";
    const string add_resurs4 = "add_resurs4";
    //-----------------------------------------------------------\\
    private string playerName;
    //--------------------------Классы--------------------------\\
    [Serializable] public class PlayerInventory : ICloneable
    {
        public int resurs1;
        public int resurs2;
        public int resurs3;
        public int resurs4;

        public object Clone()
        {
            return MemberwiseClone();
        }
    }//Класс инвентаря игрока
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

    //--------------------------Инвентарь--------------------------\\
    private PlayerInventory localPlayerInventory = new PlayerInventory
    {
        resurs1 = 0,
        resurs2 = 0,
        resurs3 = 0,
        resurs4 = 0,
    };
    private PlayerInventory changesPlayerInventory = new PlayerInventory
    {
        resurs1 = 0,
        resurs2 = 0,
        resurs3 = 0,
        resurs4 = 0,
    };
    //-----------------------------------------------------------\\
    
    public void ChangeResurs(PlayerInventory changes, string comment)//Изменить колличество ресурсов
    {
        Dictionary<string, int> resourcesChanged = new Dictionary<string, int>();

        localPlayerInventory.resurs1 += changes.resurs1;
        changesPlayerInventory.resurs1 += changes.resurs1;
        localPlayerInventory.resurs2 += changes.resurs2;
        changesPlayerInventory.resurs2 += changes.resurs2;
        localPlayerInventory.resurs3 += changes.resurs3;
        changesPlayerInventory.resurs3 += changes.resurs3;
        localPlayerInventory.resurs4 += changes.resurs4;
        changesPlayerInventory.resurs4 += changes.resurs4;

        if (changes.resurs1 != 0) resourcesChanged.Add(add_resurs1, changes.resurs1);
        if (changes.resurs2 != 0) resourcesChanged.Add(add_resurs2, changes.resurs2);
        if (changes.resurs3 != 0) resourcesChanged.Add(add_resurs3, changes.resurs3);
        if (changes.resurs4 != 0) resourcesChanged.Add(add_resurs4, changes.resurs4);

        CreateLog(comment, resourcesChanged);
    }
    public PlayerInventory GetLocalInventory()
    {
        return localPlayerInventory;
    }//Получить локальный инвентарь

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
            resurs1 = inventoryOnServer.resurs1 + changesPlayerInventory.resurs1,
            resurs2 = inventoryOnServer.resurs2 + changesPlayerInventory.resurs2,
            resurs3 = inventoryOnServer.resurs3 + changesPlayerInventory.resurs3,
            resurs4 = inventoryOnServer.resurs4 + changesPlayerInventory.resurs4,
        };//Получение корректных значений
        changesPlayerInventory = new PlayerInventory()
        {
            resurs1 = 0,
            resurs2 = 0,
            resurs3 = 0,
            resurs4 = 0,
        };//Удаление изменений
        string s1 = JsonUtility.ToJson(correctPlayerInventory);
        string s2 = JsonUtility.ToJson(localPlayerInventory);
        if (s1 != s2)
        {
            Debug.Log("The value on the local machine is different from the server value!");
            localPlayerInventory = (PlayerInventory)correctPlayerInventory.Clone();
        }
        SetInventoryOnServer(playerName, correctPlayerInventory);
    }

    //---------------------Работа с сервером---------------------\\
    static async void GetInventoryFromServer(Inventory self)//Получение инвентаря игрока с сервера
    {
        try
        {
            using var httpClient = new HttpClient();
            string url = $"https://2025.nti-gamedev.ru/api/games/d5ebfca3-ee6d-485f-9a9b-a53809bfcb62/players/{self.playerName}";
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
    static async void SetInventoryOnServer(string playerName, PlayerInventory newInventory)//Обновление инвентаря игрока на сервера
    {
        string requestUrl = $"https://2025.nti-gamedev.ru/api/games/d5ebfca3-ee6d-485f-9a9b-a53809bfcb62/players/{playerName}/";

        HttpClient httpClient = new HttpClient();
        AnswerServer resurses = new AnswerServer() {resources = newInventory};
        string inventoryJson = JsonUtility.ToJson(resurses);
        var content = new StringContent(inventoryJson, System.Text.Encoding.UTF8, "application/json");

        try
        {
            var response = await httpClient.PutAsync(requestUrl, content);

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
    static async void PostLogOnServer(Log log)//Обновление инвентаря игрока на сервера
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
                Debug.Log("Synchronization error");
            }
        }
        catch (Exception ex)
        {
            Debug.Log($"An error occurred: {ex.Message}");
        }
    }
    //-----------------------------------------------------------\\

    void Start()
    {
        playerName = PlayerPrefs.GetString("Name");
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
                    PostLogOnServer(Logs[i]);
                }
                Logs.Clear();
            }
            yield return new WaitForSeconds(5f);
        }
    }
}
