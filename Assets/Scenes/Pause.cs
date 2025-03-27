using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    [SerializeField] private List<Sprite> Sprits;
    [SerializeField] private Image image;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void OpenPause()
    {
        Time.timeScale = 0.0f;
        this.gameObject.SetActive(true);
    }
    public void ClousePause()
    {
        Time.timeScale = 1.0f;
        this.gameObject.SetActive(false);
    }
    public void OpenMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void Quit()
    {
        Application.Quit();
    }
        // Update is called once per frame
    void Update()
    {
        
    }
}
