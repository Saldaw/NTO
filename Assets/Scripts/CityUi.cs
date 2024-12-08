using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CityUi : MonoBehaviour
{
    [SerializeField] private ShopUI shop;
    [SerializeField] private Gift gift;
    [SerializeField] private GroopCreatorUI groopCreator;
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI text1;
    private List<string> names = new List<string>() {"-", "Александр Орешников", "Мария Тенелюбова", "Василий Добронравов", "Анита Тяжелая Лапа", "-", "Сунь Цзинь" };
    [SerializeField] private TextMeshProUGUI Name;
    [SerializeField] private List<Sprite> sprites_war;
    [SerializeField] private List<Sprite> sprites_deffolt;
    [SerializeField] private List<Sprite> sprites_franding;
    [SerializeField] private Image image2;
    [SerializeField] Button buttonGift;
    [SerializeField] Button buttonShop;
    private int num;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void OpenShop()
    {
        shop.Open($"City{num}Shop");
        shop.gameObject.SetActive(true);
    }
    public void OpenGift()
    {
        gift.SetBot(num);
        gift.gameObject.SetActive(true);
    }
    public void OpenWar()
    {
        groopCreator.UpdateInfo(num);
        groopCreator.gameObject.SetActive(true);
        PlayerPrefs.SetInt($"friendy{num}",10);
        UpdateInfo(num);
    }
    public void UpdateInfo(int numb)
    {
        num = numb;
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
