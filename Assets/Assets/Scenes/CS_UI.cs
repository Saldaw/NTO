using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CS_UI : MonoBehaviour
{
    const float BarWidth = 335.3745f;

    public TMP_Text LevelText;
    public GameObject BarFill;
    public GameObject UpgradeButton;

    public void Start()
    {
        // rect transform of button
        UpgradeButton.GetComponent<Button>()
            .onClick.AddListener(OnUpgrade);
    }

    public void Update()
    {
        // set the text
        LevelText.text = 
           CS_Globals.Level.ToString();

        // text of upgrade
        if (CS_Globals.Level == 3)
            UpgradeButton.transform
                .GetChild(0)
                .GetComponent<TMP_Text>()
                .text = "Эволюция";

        // getting the rect transform
        var bfrt = BarFill.GetComponent<RectTransform>();

        // setting the width
        bfrt.sizeDelta = new Vector2(
            Mathf.Clamp01(
                CS_Globals.ProgressPercent()) * BarWidth,
            bfrt.sizeDelta.y);
    }

    public void OnUpgrade()
    {
        if (CS_Globals.ProgressPercent() < 1)
            return;

        CS_Globals.Progress -=
            (uint)CS_Globals.RequiredProgress();
        CS_Globals.Level++;
        CS_Upgrade.Show = true;

        if (CS_Globals.Level == 4)
            CS_Globals.Evolve();
    }
}
