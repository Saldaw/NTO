using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PerehodCell : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private GameObject loading;
    // Start is called before the first frame update
    void Start()
    {
        bool s = CS_Globals.GrassEaten > CS_Globals.MeatyEaten;
        string a = s ? "������������" : "������������";
        string b = s ? "����" : "������";
        text.text = $"��� �������: {a}\n����� � ���������� {b}!";
    }
    public void NextLeve()
    {
        StartCoroutine(loadLevel(2));
    }
    IEnumerator loadLevel(int num)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(num);
        loading.SetActive(true);
        yield return null;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
