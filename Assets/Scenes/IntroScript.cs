using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        StartCoroutine(DelaySkip());
    }
     private IEnumerator DelaySkip()
    {
        while (true)
        {
            yield return new WaitForSeconds(58f);
            AsyncOperation operation = SceneManager.LoadSceneAsync(7);
        }
        
    }
public void Skip()
    {
        StartCoroutine(loadLevel());
    }
    IEnumerator loadLevel()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(7);
        yield return null;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
