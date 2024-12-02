using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResursInfo : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI foodText;
    [SerializeField] private TextMeshProUGUI materialsText;
    [SerializeField] private TextMeshProUGUI electronicsText;
    [SerializeField] private TextMeshProUGUI weaponsText;
    [SerializeField] private TextMeshProUGUI energyhoneyText;
    [SerializeField] private TextMeshProUGUI goldhoneyText;


    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void UodateInfo(Inventory.PlayerInventory plI)
    {
        foodText.text = plI.food.ToString();
        materialsText.text = plI.materials.ToString();
        electronicsText.text = plI.electronics.ToString();
        weaponsText.text = plI.weapons.ToString();
        energyhoneyText.text = plI.energyhoney.ToString();
        goldhoneyText.text = plI.goldhoney.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
