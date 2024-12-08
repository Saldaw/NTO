using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Net.Http;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.U2D;

public class LoginPlayer : MonoBehaviour
{
    //--------------------Unity UI переменные--------------------\\
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private TextMeshProUGUI statusText;
    [SerializeField] private TextMeshProUGUI buttonText;
    [SerializeField] private GameObject registerMenu;
    [SerializeField] private GameObject loginMenu;
    [SerializeField] private Button button;
    [SerializeField] private GameObject offlineMenu;
    //-----------------------------------------------------------\\
    [Serializable] public class Player//Класс пустого игрока 
    {
        public string name;
        public Inventory.PlayerInventory resources;
    }
    void Start()
    {
        PlayerPrefs.SetInt("Online", 1);
        PlayerPrefs.Save();
        string name = PlayerPrefs.GetString("Name");
        if (name != "")
        {
            statusText.text = "Получение данных";
            CheckNameOnServer(name, false, this);
        }
        else
        {
            statusText.text = "Регистрация";
            registerMenu.SetActive(true);
        }
    }

    public void DeliteAll() //Временная функция 
    {
        PlayerPrefs.DeleteAll();
        UnityEngine.Application.Quit();
    }

    //---------------------Работа с сервером---------------------\\
    static async void CheckNameOnServer(string username,bool isRegister, LoginPlayer self)//Проверяет существование пользователя на сервере 
    {
        try
        {
            using var httpClient = new HttpClient();
            string url = $"https://2025.nti-gamedev.ru/api/games/d5ebfca3-ee6d-485f-9a9b-a53809bfcb62/players/{username}/";
            var response = await httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Head, url));

            if (response.IsSuccessStatusCode)
            {
                if (isRegister)
                {
                    self.statusText.text = $"Аккаунт уже существует! Продолжить как {self.inputField.text}?";
                    self.loginMenu.SetActive(true);
                    self.registerMenu.SetActive(false);
                }
                else self.gameObject.SetActive(false);
            }
            else
            {
                if (isRegister)
                {
                    self.registerMenu.SetActive(false);
                    self.statusText.text = "Создание аккаунта";
                    Register(self);
                }
                else
                {
                    self.statusText.text = "Аккаунт не найден";
                    self.registerMenu.SetActive(true);
                }
            }
        }
        catch (Exception ex)
        {
            self.statusText.text = "Ошибка";
            Debug.Log($"An error occurred: {ex.Message}");
            self.offlineMenu.SetActive(true);
        }
    }
    static async void Register(LoginPlayer self)//Регистрация нового пользователя 
    {
        string requestUrl = "https://2025.nti-gamedev.ru/api/games/d5ebfca3-ee6d-485f-9a9b-a53809bfcb62/players/";

        HttpClient httpClient = new HttpClient();
        Player player = new Player() { name = $"{self.inputField.text}", resources = new Inventory.PlayerInventory() {weapons=1 } };
        string playerJson = JsonUtility.ToJson(player);
        var content = new StringContent(playerJson, System.Text.Encoding.UTF8, "application/json");

        try
        {
            var response = await httpClient.PostAsync(requestUrl, content);
            if (!response.IsSuccessStatusCode)
            {
                self.statusText.text = "Ошибка";
                self.offlineMenu.SetActive(true);
            }
            else
            {
                PlayerPrefs.SetString("Name", self.inputField.text);
                PlayerPrefs.Save();
                self.AddComponent<ShopCreator>();
                self.gameObject.SetActive(false);
            }
        }
        catch (Exception ex)
        {
            self.statusText.text = "Ошибка";
            self.offlineMenu.SetActive(true);
            Debug.Log($"An error occurred: {ex.Message}");
        }
    }
    //-----------------------------------------------------------\\

    //---------------------Оброботчик кнопок---------------------\\
    public void CheckName()//Проверяет поле ввода 
    {
        if (inputField.text == "")
        {
            button.interactable = false;
        }
        else
        {
            button.interactable = true;
        }
    }
    public void LogIn()//Вход в уже созданный аккаунт 
    {
        PlayerPrefs.SetString("Name", inputField.text);
        PlayerPrefs.Save();
        this.gameObject.SetActive(false);
    }
    public void Back()//Вернуться к регестрации 
    {
        loginMenu.SetActive(false);
        offlineMenu.SetActive(false);
        statusText.text = "Регистрация";
        registerMenu.SetActive(true);
    }
    public void ButtonClick()//Регестрирует пользователя по нажатию кнопки 
    {
        if (inputField.text != "")
        {
            registerMenu.gameObject.SetActive(false);
            statusText.text = "Получение данных";
            CheckNameOnServer(inputField.text, true, this);
        }
    }
    public void StartOffline()//Начать игру без интернета 
    {
        PlayerPrefs.SetInt("Online", 0);
        PlayerPrefs.Save();
        this.gameObject.SetActive(false);
    }
    //-----------------------------------------------------------\\
}
