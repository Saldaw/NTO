using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class CS_MenuPlac : MonoBehaviour
{
    public int etap;
    [SerializeField] List<TextMeshProUGUI> CellsText;
    [SerializeField] List<TextMeshProUGUI> ClansText;
    [SerializeField] List<TextMeshProUGUI> CevelsText;
    // Start is called before the first frame update
    void Awake()
    {

    }
    public void UpdateInfo()
    {
        if (CS_Globals.EatenByType.ContainsKey("CellOrange"))
        {
            CellsText[0].text = CS_Globals.EatenByType["CellOrange"].ToString(); //Оранжевые стрелки
        }
        if (CS_Globals.EatenByType.ContainsKey("CellRed "))
        {
            CellsText[1].text = CS_Globals.EatenByType["CellRed "].ToString(); //Красные помидорки
        }
        if (CS_Globals.EatenByType.ContainsKey("CellGreen"))
        {
            CellsText[2].text = CS_Globals.EatenByType["CellGreen"].ToString(); //Зелёные сосиски
        }
        if (CS_Globals.EatenByType.ContainsKey("CellViolet"))
        {
            CellsText[3].text = CS_Globals.EatenByType["CellViolet"].ToString(); //Лунтики
        }
        if (CS_Globals.EatenByType.ContainsKey("CellBlue"))
        {
            CellsText[4].text = CS_Globals.EatenByType["CellBlue"].ToString(); //Синие мыши
        }
        if (etap > 1)
        {
            ClansText[0].text = PlayerPrefs.GetString($"stat4","?");
            ClansText[1].text = PlayerPrefs.GetString($"stat6","?");
            ClansText[2].text = PlayerPrefs.GetString($"stat1","?");
            ClansText[3].text = PlayerPrefs.GetString($"stat3","?");
            ClansText[4].text = PlayerPrefs.GetString($"stat2","?");
        }
        if (etap > 2)
        {
            CevelsText[0].text = PlayerPrefs.GetInt($"friendy4").ToString()+"%";
            CevelsText[1].text = PlayerPrefs.GetInt($"friendy6").ToString() + "%";
            CevelsText[2].text = PlayerPrefs.GetInt($"friendy1").ToString() + "%";
            CevelsText[3].text = PlayerPrefs.GetInt($"friendy3").ToString() + "%";
            CevelsText[4].text = PlayerPrefs.GetInt($"friendy2").ToString() + "%";
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
