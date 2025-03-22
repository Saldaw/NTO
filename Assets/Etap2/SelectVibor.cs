using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectVibor : MonoBehaviour
{
    [SerializeField] GameObject loading;
    [SerializeField] private State state;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void NextLevel()
    {
        if (state.level==4)
        {
            StartCoroutine(loadLevel(1));
        }
        else
        {
            StartCoroutine(loadLevel(state.level+1+2));
        }
        
    }
    IEnumerator loadLevel(int num)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(num);
        loading.SetActive(true);
        yield return null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
