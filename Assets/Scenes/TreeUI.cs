using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TreeUI : MonoBehaviour
{
    private List<string> names = new List<string>() { "Свободно", "Александр Орешников", "Мария Тенелюбова", "Василий Добронравов", "Анита Тяжелая Лапа", "Вы", "Сунь Цзинь" };
    [SerializeField] private TextMeshProUGUI ovner;
    [SerializeField] private TextMeshProUGUI count;
    [SerializeField] private GameObject groopUI;
    private GroopController groop;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void UpdateInfo(GroopController groope)
    {
        groop = groope;
        count.text = groop.bearsHear.ToString();
        ovner.text = "владелец: " + names[groop.owner];
    }
    public void OpenGroop()
    {
        groopUI.SetActive(true);
        groopUI.GetComponent<GroopCreatorUI>().UpdateInfo(groop.point);
    }
    public void GetAll()
    {
        if (groop.owner == 5) 
        {
            groop.CreateGroop(groop.bearsHear, 5);
            groop.bearsHear = 0;
            UpdateInfo(groop);
        }
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
