using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using UnityEngine;
using UnityEngine.UI;

public class ConnectionErrorUI : MonoBehaviour
{
    List<(StringContent, string)> errorMessages;
    [SerializeField] private GameObject errorMenu;
    [SerializeField] private Button tryButton;

    public void TryAgain()
    {
        tryButton.interactable = false;
        for (int i = 0; i < errorMessages.Count; i++)
        {
            TryReqestOnServer(errorMessages[i].Item1, errorMessages[i].Item2);
        }
    }
    async void TryReqestOnServer(StringContent content, string url)//
    {
        HttpClient httpClient = new HttpClient();
        try
        {
            var response = await httpClient.PutAsync(url, content);

            if (response.IsSuccessStatusCode)
            {
                errorMessages.Remove((content, url));
                if (errorMessages.Count == 0) {errorMenu.SetActive(false); Time.timeScale = 1f;}
            }
            else
            {
                Debug.Log("Error while receiving data.");
                tryButton.interactable = true;
            }
        }
        catch (Exception ex)
        {
            Debug.Log($"An error occurred: {ex.Message}");
            tryButton.interactable = true;
        }
    }

    public void AddError(StringContent content, string url)
    {
        errorMenu.SetActive(true);
        tryButton.interactable = true;
        Time.timeScale = 0f;
        errorMessages.Add((content, url));
    }  
}
