using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CS_Upgrade : MonoBehaviour
{
    public static bool Show = false;
    public GameObject Left;
    public GameObject Right;
    public GameObject Upgrades;

    public GameObject LeftText;
    public GameObject RightText;
    public GameObject LeftIcon;
    public GameObject RightIcon;

    public Sprite UpHP;
    public Sprite UpPower;
    public Sprite UpSpeed;

    public void Start()
    {
        Upgrades.SetActive(false);

        Left.GetComponent<Button>()
            .onClick
            .AddListener(() => OnPress(true));

        Right.GetComponent<Button>()
             .onClick
             .AddListener(() => OnPress(false));
    }

    public void Update()
    {
        if (CS_Globals.Level <= 3)
            Upgrades.SetActive(Show);

        var leftText = LeftText
            .GetComponent<TMP_Text>();

        var rightText = RightText
            .GetComponent<TMP_Text>();

        var lIcon = LeftIcon
            .GetComponent<Image>();

        var rIcon = RightIcon
            .GetComponent<Image>();

        if (CS_Globals.Level == 2)
        {
            leftText.text = "Увеличить скорость\nпередвижения";
            lIcon.sprite = UpSpeed;
            rightText.text = "Увеличить урон";
            rIcon.sprite = UpPower;
        }
        else if (CS_Globals.Level == 3)
        {
            leftText.text = "Увеличить душевную силу";
            lIcon.sprite = UpHP;
            rightText.text = "Увеличить скорость\nповорота";
            rIcon.sprite = UpSpeed;
        }
    }

    void OnPress(bool left)
    {
        if (CS_Globals.Level == 2)
        {
            if (left)
                CS_Player.Speed += 50;
            else
                CS_Player.Damage += 4;
        }
        else if (CS_Globals.Level == 2)
        {
            if (left)
                /* Nothing LOL */;
            else
                CS_Player.RotationSpeed += 4;
        }

        Show = false;
    }
}
