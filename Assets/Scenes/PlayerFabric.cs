using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerFabric : MonoBehaviour
{
    public int materials = 0;
    public int electronics = 0;
    public int weapons = 0;

    private int targetCount = 0;
    private int countNow = 0;
    public bool isCreating = false;
    private DateTime startTime;
    private DateTime targetTime;

    [SerializeField] Image image;
    [SerializeField] Image image1;
    [SerializeField] Inventory inventory;
    [SerializeField] Button bn1;
    [SerializeField] Button bn2;
    [SerializeField] Button bn3;
    [SerializeField] GameObject blocks;
    // Start is called before the first frame update
    void Start()
    {
        UpdateInfo();
    }
    public void CreateOne()
    {
        StartFactory();
        
        targetCount = 1;
       
    }
    private void StartFactory()
    {
        if (PlayerPrefs.GetInt("BearsInFabric") > 0 && inventory.GetLocalInventory().energyhoney > 0)
        {
            int time = PlayerPrefs.GetInt("FabricTime") * ((2 - PlayerPrefs.GetInt("BearsInFabric")) / (PlayerPrefs.GetInt("BearsInFabric") + 5) + 1);
            startTime = DateTime.Now;
            targetTime = startTime.AddSeconds(time);

            blocks.SetActive(false);
            bn1.gameObject.SetActive(false);
            bn2.gameObject.SetActive(false);
            bn3.enabled = false;
            countNow = 0;
            isCreating = true;
        }
    }
    public void CreateAll()
    {
        StartFactory();
        targetCount = -1;
    }
    public void UpdateInfo()
    {
        image1.fillAmount = (float)PlayerPrefs.GetInt("BearsInFabric") / PlayerPrefs.GetInt("MaxBearsInFabric");
    } 
    private void Finish()
    {
        inventory.ChangeResurs(new Inventory.PlayerInventory() { weapons = weapons, electronics = electronics, materials = materials, energyhoney = -1 }, "The plant successfully creates resources");
        countNow++;
        if (targetCount == -1 || countNow < targetCount)
        {
            if (inventory.GetLocalInventory().energyhoney > 0)
            {
                int time = PlayerPrefs.GetInt("FabricTime") * ((2 - PlayerPrefs.GetInt("BearsInFabric")) / (PlayerPrefs.GetInt("BearsInFabric") + 5) + 1);
                startTime = DateTime.Now;
                targetTime = startTime.AddSeconds(time);
                isCreating = true;
            }
        }
        else 
        { 
            Stop(); 
        }

    }
    public void Stop()
    {
        blocks.SetActive(true);
        bn1.gameObject.SetActive(true);
        bn2.gameObject.SetActive(true);
        bn3.enabled = true;
        isCreating = false;
        image.fillAmount = 0;
    }
    // Update is called once per frame
    void Update()
    {
        if (isCreating)
        {
            if (DateTime.Now.Ticks - startTime.Ticks <= targetTime.Ticks - startTime.Ticks)
            {
                image.fillAmount = (float)(DateTime.Now.Ticks - startTime.Ticks) / (targetTime.Ticks - startTime.Ticks);
            }
            else
            {
                isCreating = false;
                Finish();
            }
        }
    }
}
