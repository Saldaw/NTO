using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gift : MonoBehaviour
{
    [SerializeField] private Inventory playerInventory;
    private int num;
    public void sendFirstGift()
    {
        if (playerInventory.GetLocalInventory().goldhoney >= 20 && PlayerPrefs.GetInt($"friendy{num}")<=99)
        {
            playerInventory.ChangeResurs(new Inventory.PlayerInventory() { goldhoney = -20 }, "the player gives a gift");
            PlayerPrefs.SetInt($"friendy{num}", PlayerPrefs.GetInt($"friendy{num}") + 1);
        }
    }
    public void sendSecondGift()
    {
        if (playerInventory.GetLocalInventory().goldhoney >= 50 && PlayerPrefs.GetInt($"friendy{num}") <= 97)
        {
            playerInventory.ChangeResurs(new Inventory.PlayerInventory() { goldhoney = -50 }, "the player gives a gift");
            PlayerPrefs.SetInt($"friendy{num}", PlayerPrefs.GetInt($"friendy{num}") + 3);
        }
    }
    public void sendThirdGift()
    {
        if (playerInventory.GetLocalInventory().goldhoney >= 100 && PlayerPrefs.GetInt($"friendy{num}") <= 90)
        {
            playerInventory.ChangeResurs(new Inventory.PlayerInventory() { goldhoney = -100 }, "the player gives a gift");
            PlayerPrefs.SetInt($"friendy{num}", PlayerPrefs.GetInt($"friendy{num}") + 10);
        }
    }
    public void SetBot(int i)
    {
        num = i;
    }
}
