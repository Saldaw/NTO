using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject mainButtonHolder;

    [SerializeField] private GameObject settingsButtonHolder;
    [SerializeField] private GameObject settingsMenu;

    [SerializeField] private GameObject graphicsMenu;
    [SerializeField] private GameObject audioMenu;

    private void Awake()
    {
        CloseSettings();
    }

    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
        Application.Quit();
    }

    //это если нужно будет использовать это же меню в качестве меню настроек
    public void Resume()
    {
        // напишешь сам код выхода с паузы
    }

    public void OpenSettings()
    {
        mainButtonHolder.SetActive(false);
        settingsMenu.SetActive(true);
        settingsButtonHolder.SetActive(true);

        OpenGraphicsMenu();
    }

    public void OpenGraphicsMenu()
    {
        graphicsMenu.SetActive(true);
        audioMenu.SetActive(false);
    }

    public void OpenAudioMenu()
    {
        audioMenu.SetActive(true);
        graphicsMenu.SetActive(false);
    }

    public void CloseSettings()
    {
        mainButtonHolder.SetActive(true);
        settingsMenu.SetActive(false);
        settingsButtonHolder.SetActive(false);
        graphicsMenu.SetActive(false);
        audioMenu.SetActive(false);
    }
}
