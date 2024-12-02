using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectCount : MonoBehaviour
{
    [SerializeField] Image prograssBar1;
    [SerializeField] Image prograssBar2;
    [SerializeField] Image prograssBar3;
    [SerializeField] PlayerFabric fabric;
    private int bear1;
    private int bear2;
    private int bear3;
    private int bear1max;
    private int bear2max;
    private int bear3max;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void UpdateInfo()
    {
        bear1 = PlayerPrefs.GetInt("BearsInFabric");
        bear1max = PlayerPrefs.GetInt("MaxBearsInFabric");
        bear2 = PlayerPrefs.GetInt("BearsOnFarm");
        bear2max = PlayerPrefs.GetInt("MaxBearsOnFarm");
        bear3 = PlayerPrefs.GetInt("countLiversIdle5");
        bear3max = PlayerPrefs.GetInt("countLivers5");

        prograssBar1.fillAmount = (float)bear1 / bear1max;
        prograssBar2.fillAmount = (float)bear2 / bear2max;
        prograssBar3.fillAmount = (float)bear3 / bear3max;
    }
    public void CangeFabric(int i)
    {
        if(bear1+i<=bear1max && bear1 + i >= 0 && bear3-i>=0)
        {
            bear1 += i;
            prograssBar1.fillAmount = (float)bear1/bear1max;
            bear3 -= i;
            fabric.Stop();
        }
        prograssBar3.fillAmount=(float)bear3/bear3max;
    }
    public void CangeFarms(int i)
    {
        if (bear2 + i <= bear2max && bear2 + i >= 0 && bear3 - i >= 0)
        {
            bear2 += i;
            prograssBar2.fillAmount = (float)bear2 / bear2max;
            bear3 -= i;
        }
        prograssBar3.fillAmount = (float)bear3 / bear3max;
    }
    public void SaveChanges()
    {
        PlayerPrefs.SetInt("BearsInFabric", bear1);
        PlayerPrefs.SetInt("BearsOnFarm", bear2);
        PlayerPrefs.SetInt("countLiversIdle5", bear3);
        fabric.UpdateInfo();
        this.gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
