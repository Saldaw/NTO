using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityParametr : MonoBehaviour
{
    private int countLivers;
    private int defense;

    private int friendy;
    private int trade;

    private int force;
    private int countHony;

    [SerializeField] private int numSity;

    void Start()
    {
        PlayerPrefs.DeleteKey($"friendy{numSity}");
        if (PlayerPrefs.HasKey($"friendy{numSity}"))
        {
            friendy = PlayerPrefs.GetInt($"friendy{numSity}");
            trade = PlayerPrefs.GetInt($"trade{numSity}");
            countLivers = PlayerPrefs.GetInt($"countLivers{numSity}");
            defense = PlayerPrefs.GetInt($"defense{numSity}");
            force = PlayerPrefs.GetInt($"force{numSity}");
            countHony = PlayerPrefs.GetInt($"countHony{numSity}");
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
        PlayerPrefs.SetInt($"friendy{numSity}", friendy);
        PlayerPrefs.SetInt($"trade{numSity}", trade);
        PlayerPrefs.SetInt($"countLivers{numSity}", countLivers);
        PlayerPrefs.SetInt($"defense{numSity}", defense);
        PlayerPrefs.SetInt($"force{numSity}",1);
        PlayerPrefs.SetInt($"countHony{numSity}", 0);
    }
}
