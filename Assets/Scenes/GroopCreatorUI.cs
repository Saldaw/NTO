using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class GroopCreatorUI : MonoBehaviour
{
    [SerializeField] private GroopController playerGC;
    [SerializeField] private CityParametr playerParametrs;
    [SerializeField] private Inventory inventory;
    [SerializeField] private Slider slider;
    [SerializeField] private TextMeshProUGUI bearCountText;
    [SerializeField] private TextMeshProUGUI weaponCountText;
    [SerializeField] private TextMeshProUGUI createButtonText;
    [SerializeField] private Button createButton;

    private int countBear;
    private Inventory.PlayerInventory resurses;

    private int pointToGo;
    void Start()
    {
        
    }
    public void ChangeSlider()
    {
        if (slider.value != 0 && slider.value <= Mathf.Min(countBear, resurses.weapons) && countBear * resurses.weapons != 0)
        {
            createButton.gameObject.SetActive(true);
            createButton.interactable = true;
            createButtonText.text = "Отправить: "+ slider.value.ToString();
        }
        else 
        { 
            createButton.gameObject.SetActive(false);
        }
    }
    public void UpdateInfo(int point)
    {
        createButton.gameObject.SetActive(false);
        resurses = inventory.GetLocalInventory();
        countBear = PlayerPrefs.GetInt("countLiversIdle5");
        bearCountText.text = "Медведей: "+countBear.ToString();
        weaponCountText.text = "Оружия: " + resurses.weapons.ToString();
        pointToGo = point;
        slider.value = 0;
        if (countBear == 0 || resurses.weapons == 0)
        {
            slider.interactable = false;
        }
        else 
        { 
            slider.maxValue = Mathf.Min(countBear, resurses.weapons);
            slider.interactable = true;
        }
    }
    public void SendGroop()
    {
        playerGC.CreateGroop((int)slider.value, pointToGo);
        inventory.ChangeResurs(new Inventory.PlayerInventory() { weapons = -resurses.weapons }, "A group was sent");
        PlayerPrefs.SetInt("countLiversIdle5", PlayerPrefs.GetInt("countLiversIdle5") - (int)slider.value);
        this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
