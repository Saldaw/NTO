using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectCount : MonoBehaviour
{
    [SerializeField] Image prograssBar1;
    [SerializeField] Image prograssBar2;
    [SerializeField] Image prograssBar3;
    [SerializeField] Inventory inventory;
    [SerializeField] PlayerFabric fabric;
    [SerializeField] TextMeshProUGUI countText1;
    [SerializeField] TextMeshProUGUI countText2;
    [SerializeField] TextMeshProUGUI countText3;

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
    public void AddBear()
    {
        if(inventory.GetLocalInventory().food >= 1){
            inventory.ChangeResurs(new Inventory.PlayerInventory() { food = -1 }, "A bear was created");
            PlayerPrefs.SetInt("countLiversIdle5", PlayerPrefs.GetInt("countLiversIdle5") + 1);
            PlayerPrefs.SetInt("countLivers5", PlayerPrefs.GetInt("countLivers5") + 1);
            UpdateInfo();
        }
    }
    public void UpdateInfo()
    {
        bear1 = PlayerPrefs.GetInt("BearsInFabric");
        bear1max = PlayerPrefs.GetInt("MaxBearsInFabric");
        bear2 = PlayerPrefs.GetInt("BearsOnFarm");
        bear2max = PlayerPrefs.GetInt("MaxBearsOnFarm");
        bear3 = PlayerPrefs.GetInt("countLiversIdle5");
        bear3max = PlayerPrefs.GetInt("countLivers5");

        countText1.text = bear1.ToString();
        countText2.text = bear2.ToString();
        countText3.text = bear3.ToString();
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
