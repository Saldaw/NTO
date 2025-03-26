using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Build : MonoBehaviour
{
    [SerializeField] private State state;
    [SerializeField] private Image progBar;
    [SerializeField] private GameObject build;
    [SerializeField] private List<GameObject> builds;
    [SerializeField] private List<int> HPS;
    [SerializeField] private int HP;
    [SerializeField] private int MaxHP;
    [SerializeField] public int type=0;
    [SerializeField] private GameObject ariaPref;
    [SerializeField] private GameObject aria;
    [SerializeField] private BuildMenu BuildingMenu;
    [SerializeField] private GameObject Unitmenu;
    [SerializeField] private GameObject HPBar;

    private float WoodSpeed = 0;
    private float HoneySpeed = 0;

    public void SetBuilding(int newType)
    {
        type = newType;
        Destroy(build);
        build = Instantiate(builds[newType],this.transform);
        StopAllCoroutines();
        HP = MaxHP = HPS[newType];
        if (MaxHP>0)
        {
            progBar.fillAmount = 1f * HP / MaxHP;
            state.playerBuilds.Add(this);
            HPBar.SetActive(true);
        }
        else
        {
            HPBar.SetActive(false);
        }
        if (aria)
        {
            Destroy(aria);
        }
        if(type==2)
        {
            aria = Instantiate(ariaPref,transform);
        }
        
        if (type == 3)
        {
            StartCoroutine(SpawnHoney());
        }
        if (type == 4)
        {
            StartCoroutine(SpawnWood());
        }
    }
    public void GetDamage(int damage)
    {
        HP -= damage;
        progBar.fillAmount = 1f*HP / MaxHP;
        if (HP <= 0)
        {
            if (type == 1)
            {
                state.End();
            }
            else
            {
                state.playerBuilds.Remove(this);
                SetBuilding(0);
            }
        }
    }
    private IEnumerator SpawnHoney()
    {
        while (true)
        {
            yield return new WaitForSeconds(10f+ HoneySpeed);
            state.honey += 10+state.level*10;
            state.UpdateResurs();
            
        }
    }
    private IEnumerator SpawnWood()
    {
        while (true)
        {
            yield return new WaitForSeconds(10f+ WoodSpeed);
            state.wood += 10 + state.level * 10;
            state.UpdateResurs();
        }
    }
    public void OnMouseDown() 
    { 
        if (type == 0)
            {
            Unitmenu.SetActive(false);
            BuildingMenu.build = this;
            BuildingMenu.gameObject.SetActive(true);
            }
        if(type == 1)
        {
            BuildingMenu.gameObject.SetActive(false);
            Unitmenu.SetActive(true);
        }
    }  // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < Mod.mods.Count; i++)
        {
            if (Mod.mods[i].clan != null)
            {
                HoneySpeed += Mod.mods[i].clan.HoneySpeed;
                WoodSpeed += (int)Mod.mods[i].clan.WoodSpeed;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
