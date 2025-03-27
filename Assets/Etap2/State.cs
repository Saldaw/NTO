using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class State : MonoBehaviour
{
    public int honey;
    public int wood;
    public int level;
    public int numVilage;
    public NotPlayerBuild targetBuild;
    public GameObject Home;
    public List<Unit> playerUnits = new List<Unit>();
    public List<Build> playerBuilds = new List<Build>();
    [SerializeField] private TextMeshProUGUI honytxt;
    [SerializeField] private TextMeshProUGUI woodtxt;
    [SerializeField] private GameObject vibor;
    [SerializeField] private GameObject endmenu;
    [SerializeField] private GameObject loading;
    [SerializeField] private Image icon;
    public List<GameObject> Points;
    [SerializeField] private GameObject woodpref;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnWood());
        UpdateResurs();
        for (int i = 0; i < Mod.mods.Count; i++)
        {
            if (Mod.mods[i].Type == 2)
            {
                icon.sprite = Mod.mods[i].clan.Icon;
            }
        }
      
        Time.timeScale = 1.0f;
        if (CS_Globals.MeatyEaten >= CS_Globals.GrassEaten)
        {
            wood = (int)(wood * 1.2f);
        }
        else
        {
            honey = (int)(honey * 1.2f);
        }
        UpdateResurs();
    }
    public void UpdateResurs()
    {
        honytxt.text = honey.ToString();
        woodtxt.text = wood.ToString();
    }
    public void SetTargetBuild(NotPlayerBuild build)
    {
        if (targetBuild)
        {
            targetBuild.decal.SetActive(false);
        }
        targetBuild = build;
        for (int i = 0; i < playerUnits.Count; i++)
        {
            playerUnits[i].SetTarget(build.gameObject);
        }
        targetBuild.decal.SetActive(true);
    }
    public void GoHome()
    {
        if (targetBuild)
        {
            targetBuild.decal.SetActive(false);
        }
        targetBuild = null;
        for (int i = 0; i < playerUnits.Count; i++)
        {
            Instantiate(Points[Random.Range(0, Points.Count)]);
            playerUnits[i].SetTarget(Home.gameObject);
        }
    }
    public void Win()
    {
        Time.timeScale = 0f;
        vibor.SetActive(true);
    }
    public void End()
    {
        Time.timeScale = 0f;
        endmenu.SetActive(true);
    }
    public void Pause()
    {
        Time.timeScale = 0f;
    }
    public void PauseOff()
    {
        Time.timeScale = 1f;
    }
    public void Restart()
    {
        StartCoroutine(loadLevel(level+2));
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
    IEnumerator SpawnWood()
    {
        while (true)
        {
            Instantiate(woodpref, Points[Random.Range(0, Points.Count)].transform.position,new Quaternion());
            yield return new WaitForSeconds (Random.Range(4,10));
        }
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
