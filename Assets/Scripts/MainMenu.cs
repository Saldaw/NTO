using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject loading;
    [SerializeField] private GameObject mainButtonHolder;

    [SerializeField] private GameObject settingsButtonHolder;
    [SerializeField] private GameObject settingsMenu;

    [SerializeField] private GameObject graphicsMenu;
    [SerializeField] private GameObject audioMenu;

    private void Awake()
    {
        CloseSettings();
        Time.timeScale = 1.0f;
        PlayerPrefs.DeleteAll();
    }

    public void Play()
    {
        StartCoroutine(loadLevel());
    }
    IEnumerator loadLevel()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(7);
        loading.SetActive(true);
        yield return null;
    }

    public void Exit()
    {
        Application.Quit();
    }

    //��� ���� ����� ����� ������������ ��� �� ���� � �������� ���� ��������
    public void Resume()
    {
        // �������� ��� ��� ������ � �����
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
