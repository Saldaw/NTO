using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiker : MonoBehaviour
{
    public List<Groop> groops = new List<Groop>();
    [SerializeField] private float timeMove=5f;
    [SerializeField] private float timeHony = 30f;
    [SerializeField] Inventory plyerInventory;
    [SerializeField] private Pause pause;
    [SerializeField] private GameObject Win;
    [SerializeField] private GameObject End;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(TiksMovingCorutin());
        StartCoroutine(TiksHonyCorutin());
        StartCoroutine(TiksFoodCorutin());
    }
    private IEnumerator TiksMovingCorutin()
    {
        while (true)
        {
            TiksMoving();
            if ((PlayerPrefs.GetInt("friendy1") >= 90 || PlayerPrefs.GetInt("isDie1") == 1) && (PlayerPrefs.GetInt("friendy2") >= 90 || PlayerPrefs.GetInt("isDie2") == 1) && (PlayerPrefs.GetInt("friendy3") >= 90 || PlayerPrefs.GetInt("isDie3") == 1) && (PlayerPrefs.GetInt("friendy4") >= 90 || PlayerPrefs.GetInt("isDie4") == 1) && (PlayerPrefs.GetInt("friendy6") >= 90 || PlayerPrefs.GetInt("isDie6") == 1))
            {
                Win.SetActive(true);
                Time.timeScale = 0f;
            }
            else if (PlayerPrefs.GetInt("isDie5") == 1)
            {
                End.SetActive(true);
                Time.timeScale = 0f;
            }
            yield return new WaitForSeconds(timeMove);
        }
    }
    private IEnumerator TiksHonyCorutin()
    {
        while (true)
        {
            TiksHony();
            yield return new WaitForSeconds(timeHony);
        }
    }
    private IEnumerator TiksFoodCorutin()
    {
        while (true)
        {
            TiksFood();
            yield return new WaitForSeconds(timeHony);
        }
    }
    private void TiksMoving()
    {
        for (int i = 0; i < groops.Count; i++)
        {
            groops[i].groopImage.SinhronPosition();
        }
    }
    private void TiksHony()
    {
        if (PlayerPrefs.GetInt("countHony5") != 0)
        {
            plyerInventory.ChangeResurs(new Inventory.PlayerInventory() { energyhoney = PlayerPrefs.GetInt("countHony5") }, "Honey was collected from the player's trees");
        }   
    }
    private void TiksFood()
    {
        if (PlayerPrefs.GetInt("BearsOnFarm") != 0)
        {
            plyerInventory.ChangeResurs(new Inventory.PlayerInventory() { food = PlayerPrefs.GetInt("BearsOnFarm")/5+1 }, "Farms produce a harvest");
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !pause.gameObject.active)
        {
            pause.OpenPause();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && pause.gameObject.active)
        {
            pause.ClousePause();
        }
    }
}
