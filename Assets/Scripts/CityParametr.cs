using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityParametr : MonoBehaviour
{
    private int countLivers;
    private int countLiversIdle;
    private int defense;

    private int friendy;
    private int trade;

    private int force;
    private int countHony;
    private int isDie;

    [SerializeField] private int numSity;
    [SerializeField] private GameObject groopCreatorUI;
    [SerializeField] private GameObject plauerUI;
    [SerializeField] private GameObject CityUI;
    private void OnMouseUp()
    {
        if (numSity == 5)
        {
            plauerUI.SetActive(true);
        }
        else
        {
            CityUI.GetComponent<CityUi>().UpdateInfo(numSity);
            CityUI.SetActive(true);
        }
    }
    void Start()
    {
        if (PlayerPrefs.HasKey($"friendy{numSity}"))
        {
            isDie = PlayerPrefs.GetInt($"isDie{numSity}");
            friendy = PlayerPrefs.GetInt($"friendy{numSity}");
            trade = PlayerPrefs.GetInt($"trade{numSity}");
            countLivers = PlayerPrefs.GetInt($"countLivers{numSity}");
            defense = PlayerPrefs.GetInt($"defense{numSity}");
            force = PlayerPrefs.GetInt($"force{numSity}");
            countHony = PlayerPrefs.GetInt($"countHony{numSity}");
            countLiversIdle = PlayerPrefs.GetInt($"countLiversIdle{numSity}");
        }
        else
        {
            GenerateParametrs();
        }
    }
    private void GenerateParametrs()
    {
        countLivers = Random.Range(10, 20);
        defense = Random.Range(10, 20);
        friendy = Random.Range(40, 60);
        trade = Random.Range(40, 60);
        SaveParametrs();

    }
    public void SaveParametrs()
    {
        PlayerPrefs.SetInt($"isDie{numSity}",0);
        PlayerPrefs.SetInt($"friendy{numSity}", friendy);
        PlayerPrefs.SetInt($"trade{numSity}", trade);
        PlayerPrefs.SetInt($"countLivers{numSity}", countLivers);
        PlayerPrefs.SetInt($"defense{numSity}", defense);
        PlayerPrefs.SetInt($"force{numSity}",1);
        PlayerPrefs.SetInt($"countHony{numSity}", 0);
        PlayerPrefs.SetInt($"countLiversIdle{numSity}", countLivers);
        if (numSity == 5)
        {
            PlayerPrefs.SetInt("BearsOnFarm", 0);
            PlayerPrefs.SetInt("MaxBearsOnFarm", 5);
            PlayerPrefs.SetInt("BearsInFabric", 0);
            PlayerPrefs.SetInt("MaxBearsInFabric", 10);
            PlayerPrefs.SetInt("FabricTime", 10);
            PlayerPrefs.Save();
        }
    }
}
