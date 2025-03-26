using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CS_Menu : MonoBehaviour
{
    [SerializeField] GameObject loading;
    public void Pause()
    {
        Time.timeScale = 0f;
    }
    public void PauseOff()
    {
        Time.timeScale = 1f;
    }
    public void ToMenu()
    {
        StartCoroutine(loadLevel(0));
    }
    public void OpenLevel(int i)
    {
        StartCoroutine(loadLevel(i));
    }
    IEnumerator loadLevel(int num)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(num);
        loading.SetActive(true);
        yield return null;
    }
}
