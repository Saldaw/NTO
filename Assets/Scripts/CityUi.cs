using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CityUi : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI text1;
    private List<string> names = new List<string>() {"-","Александр","-", "-", "-", "-", "-" };
    [SerializeField] private TextMeshProUGUI Name;
    [SerializeField] private List<Sprite> sprites_war;
    [SerializeField] private List<Sprite> sprites_deffolt;
    [SerializeField] private List<Sprite> sprites_franding;
    [SerializeField] private Image image2;
    [SerializeField] Button buttonGift;
    [SerializeField] Button buttonShop;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void UpdateInfo(int num)
    {
        int friendy = PlayerPrefs.GetInt($"friendy{num}");
        image.fillAmount = friendy / 100f;
        text1.text = friendy.ToString();
        Name.text = names[num];
        if (friendy < 30)
        {
            image.color = Color.red;
            image2.sprite = sprites_war[num];
            buttonGift.enabled = false;
            buttonShop.enabled = false;
        }
        else if (friendy > 70)
        {
            image.color = Color.green;
            image2.sprite = sprites_franding[num];
            buttonGift.enabled = true;
            buttonShop.enabled = true;
        }
        else
        {
            image.color = Color.yellow;
            image2.sprite = sprites_deffolt[num];
            buttonGift.enabled = true;
            buttonShop.enabled = true;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
