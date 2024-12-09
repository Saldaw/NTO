using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public List<GameObject> gameObjects;
    private int tutorNow=0;
    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.GetInt("Tutorial",0) == 0)
        {
            gameObjects[0].SetActive(true);
        }
    }
    public void NextTutorial()
    {
        if (PlayerPrefs.GetInt("Tutorial", 0) == 0)
        {
            gameObjects[tutorNow].SetActive(false);
            tutorNow++;
            if (tutorNow < gameObjects.Count)
            {
                gameObjects[tutorNow].SetActive(true);
            }
            else
            {
                PlayerPrefs.SetInt("Tutorial", 1);
            }
        }
    }
    public void PreviousTutorial()
    {
        gameObjects[tutorNow].SetActive(false);
        tutorNow--;
        if (tutorNow < gameObjects.Count)
        {
            gameObjects[tutorNow].SetActive(true);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
