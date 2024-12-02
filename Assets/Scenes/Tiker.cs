using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiker : MonoBehaviour
{
    public List<Groop> groops = new List<Groop>();
    [SerializeField] private float timeMove=5f;
    [SerializeField] private float timeHony = 30f;
    [SerializeField] Inventory plyerInventory;
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
            plyerInventory.ChangeResurs(new Inventory.PlayerInventory() { food = PlayerPrefs.GetInt("BearsOnFarm") }, "Farms produce a harvest");
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
