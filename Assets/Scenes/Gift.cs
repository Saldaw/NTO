using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Gift : MonoBehaviour
{
    [SerializeField] private Inventory playerInventory;
    [SerializeField] private TextMeshProUGUI cost1;
    [SerializeField] private TextMeshProUGUI cost2;
    [SerializeField] private TextMeshProUGUI cost3;
    private int num;
    public void sendFirstGift()
    {
        if (playerInventory.GetLocalInventory().goldhoney >= PlayerPrefs.GetInt($"Gift1{num}") && PlayerPrefs.GetInt($"friendy{num}")<=99)
        {
            playerInventory.ChangeResurs(new Inventory.PlayerInventory() { goldhoney = -PlayerPrefs.GetInt($"Gift1{num}") }, "the player gives a gift");
            PlayerPrefs.SetInt($"friendy{num}", PlayerPrefs.GetInt($"friendy{num}") + 1);
        }
    }
    public void sendSecondGift()
    {
        if (playerInventory.GetLocalInventory().goldhoney >= PlayerPrefs.GetInt($"Gift2{num}") && PlayerPrefs.GetInt($"friendy{num}") <= 97)
        {
            playerInventory.ChangeResurs(new Inventory.PlayerInventory() { goldhoney = -PlayerPrefs.GetInt($"Gift2{num}") }, "the player gives a gift");
            PlayerPrefs.SetInt($"friendy{num}", PlayerPrefs.GetInt($"friendy{num}") + 3);
        }
    }
    public void sendThirdGift()
    {
        if (playerInventory.GetLocalInventory().goldhoney >= PlayerPrefs.GetInt($"Gift3{num}") && PlayerPrefs.GetInt($"friendy{num}") <= 90)
        {
            playerInventory.ChangeResurs(new Inventory.PlayerInventory() { goldhoney = -PlayerPrefs.GetInt($"Gift3{num}") }, "the player gives a gift");
            PlayerPrefs.SetInt($"friendy{num}", PlayerPrefs.GetInt($"friendy{num}") + 10);
        }
    }
    public void SetBot(int i)
    {
        num = i;
        cost1.text = PlayerPrefs.GetInt($"Gift1{num}").ToString();
        cost2.text = PlayerPrefs.GetInt($"Gift2{num}").ToString();
        cost3.text = PlayerPrefs.GetInt($"Gift3{num}").ToString();
    }
}
