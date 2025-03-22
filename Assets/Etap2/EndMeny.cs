using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndMeny : MonoBehaviour
{
    [SerializeField] GameObject loading;
    [SerializeField] private State state;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void RestartLevel()
    {
        StartCoroutine(loadLevel(state.level + 2));
    }
    public void GoOut()
    {
        StartCoroutine(loadLevel(0));
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
