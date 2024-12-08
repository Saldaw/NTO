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
    //--------------------Unity UI ����������--------------------\\
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private TextMeshProUGUI statusText;
    [SerializeField] private TextMeshProUGUI buttonText;
    [SerializeField] private GameObject registerMenu;
    [SerializeField] private GameObject loginMenu;
    [SerializeField] private Button button;
    [SerializeField] private GameObject offlineMenu;
    //-----------------------------------------------------------\\
    [Serializable] public class Player//����� ������� ������ 
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
            statusText.text = "��������� ������";
            CheckNameOnServer(name, false, this);
        }
        else
        {
            statusText.text = "�����������";
            registerMenu.SetActive(true);
        }
    }

    public void DeliteAll() //��������� ������� 
    {
        PlayerPrefs.DeleteAll();
        UnityEngine.Application.Quit();
    }

    //---------------------������ � ��������---------------------\\
    static async void CheckNameOnServer(string username,bool isRegister, LoginPlayer self)//��������� ������������� ������������ �� ������� 
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
                    self.statusText.text = $"������� ��� ����������! ���������� ��� {self.inputField.text}?";
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
                    self.statusText.text = "�������� ��������";
                    Register(self);
                }
                else
                {
                    self.statusText.text = "������� �� ������";
                    self.registerMenu.SetActive(true);
                }
            }
        }
        catch (Exception ex)
        {
            self.statusText.text = "������";
            Debug.Log($"An error occurred: {ex.Message}");
            self.offlineMenu.SetActive(true);
        }
    }
    static async void Register(LoginPlayer self)//����������� ������ ������������ 
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
                self.statusText.text = "������";
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
            self.statusText.text = "������";
            self.offlineMenu.SetActive(true);
            Debug.Log($"An error occurred: {ex.Message}");
        }
    }
    //-----------------------------------------------------------\\

    //---------------------���������� ������---------------------\\
    public void CheckName()//��������� ���� ����� 
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
    public void LogIn()//���� � ��� ��������� ������� 
    {
        PlayerPrefs.SetString("Name", inputField.text);
        PlayerPrefs.Save();
        this.gameObject.SetActive(false);
    }
    public void Back()//��������� � ����������� 
    {
        loginMenu.SetActive(false);
        offlineMenu.SetActive(false);
        statusText.text = "�����������";
        registerMenu.SetActive(true);
    }
    public void ButtonClick()//������������ ������������ �� ������� ������ 
    {
        if (inputField.text != "")
        {
            registerMenu.gameObject.SetActive(false);
            statusText.text = "��������� ������";
            CheckNameOnServer(inputField.text, true, this);
        }
    }
    public void StartOffline()//������ ���� ��� ��������� 
    {
        PlayerPrefs.SetInt("Online", 0);
        PlayerPrefs.Save();
        this.gameObject.SetActive(false);
    }
    //-----------------------------------------------------------\\
}
