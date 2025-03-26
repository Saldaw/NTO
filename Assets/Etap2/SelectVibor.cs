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
    public void NextLevel(int i)
    {
        switch (i)
        {
            case 0:
                PlayerPrefs.SetInt($"friendy{state.numVilage}", 20);
                break;
            case 2:
                PlayerPrefs.SetInt($"friendy{state.numVilage}", 65);
                break;
        }
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
