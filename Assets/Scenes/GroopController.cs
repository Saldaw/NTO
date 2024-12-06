using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class GroopController : MonoBehaviour
{
    public Dictionary<int, Vector3> allPoints = new Dictionary<int, Vector3> { { 1, new Vector3(-169+1522.313f, -6.3f + 8.911863f, 180+ 1373.025f) }, { 2, new Vector3(-21+1522.313f, -5 + 8.911863f, -20 + 1373.025f) }, { 3, new Vector3(226+1522.313f, -9 + 8.911863f, 48 + 1373.025f) }, { 4, new Vector3(200 + 1522.313f, -9 + 8.911863f, 300 + 1373.025f) }, { 5, new Vector3(1546, -2, 1622) }, { 6, new Vector3(-290 + 1522.313f, -6.5f + 8.911863f, 307 + 1373.025f) },{10, new Vector3(1317.78003f, 1.83f, 1672.93994f) },{11,new Vector3(1188.34998f, 5.88199997f, 1618.35999f) },{12, new Vector3(1264.69995f, 2.24000001f, 1554.31995f) },{13, new Vector3(1395.71997f, 3.23f, 1285.73999f) },{14, new Vector3(1509.83972f, 5.3f, 1333.70764f) },{15, new Vector3(1672.6676f, 1.65f, 1296.70508f) },{16,new Vector3(1776.08997f, -1.85f, 1653.62f) },{17,new Vector3(1768.03003f, -1.16f, 1579.29f) },{18,new Vector3(1639.37f, 1.85f, 1502.38f) },{19,new Vector3(1430.89f, 7.95f, 1659.79f) } };
    [SerializeField] private GameObject groopMove;
    [SerializeField] private GameObject groopUI;
    [SerializeField] private Tiker tiker;
    [SerializeField] private int point;
    [SerializeField] private int owner;
    [SerializeField] private Inventory inventory;
    public int bearsHear;
    [SerializeField] int type;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public Groop CreateGroop(int count, int targetPoint)
    {
        GameObject gmGroopUI = Instantiate(groopUI,transform.position, Quaternion.Euler(90,0,0));
        GameObject gmGroopMove = Instantiate(groopMove, transform.position, Quaternion.Euler(0, 0, 0));
        Groop groop = new Groop() {groopImage = gmGroopUI.GetComponent<GroopImage>(),playerNavigation = gmGroopMove.GetComponent<PlayerNavigation>(), gmGroopUI = gmGroopUI, gmGroopMove = gmGroopMove};
        groop.groopImage.targetPosition = gmGroopMove;
        groop.groopImage.StartPosition = transform.position;
        groop.groopImage.targetWalking = targetPoint;
        groop.groopImage.startWalking = point;
        groop.groopImage.owner = owner;
        groop.groopImage.SetInfo(count, groop,this);
        groop.playerNavigation.SetTargetPosition(allPoints[targetPoint]);
        tiker.groops.Add(groop);
        return groop;
    }
    public void DestroyGroop(Groop groop)
    {
        tiker.groops.Remove(groop);
        Destroy(groop.gmGroopMove,1f);
        Destroy(groop.gmGroopUI, 1f);
    }
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        GroopImage groopInTriger = other.GetComponent<GroopImage>();
        if (groopInTriger && groopInTriger.targetWalking == point && owner==groopInTriger.owner) 
        { 
            bearsHear += groopInTriger.countBear; 
            if(type == 1)
            {
                PlayerPrefs.SetInt($"countLiversIdle{owner}", PlayerPrefs.GetInt($"countLiversIdle{owner}") + bearsHear);
                if (owner == 5) inventory.ChangeResurs(new Inventory.PlayerInventory() { weapons = groopInTriger.countBear }, "The bears are back with weapons");
                bearsHear = 0;
            }
            DestroyGroop(groopInTriger.thisGroop);
        }
        else if (groopInTriger && groopInTriger.targetWalking == point) 
        {
            if (type == 1 && groopInTriger.countBear * PlayerPrefs.GetInt($"force{groopInTriger.owner}") <= PlayerPrefs.GetInt($"countLiversIdle{owner}") + PlayerPrefs.GetInt($"defense{owner}"))
            {
                PlayerPrefs.SetInt($"countLiversIdle{owner}", PlayerPrefs.GetInt($"countLiversIdle{owner}")- groopInTriger.countBear);
                DestroyGroop(groopInTriger.thisGroop);
            }
            else if(type == 1 && groopInTriger.countBear * PlayerPrefs.GetInt($"force{groopInTriger.owner}") > PlayerPrefs.GetInt($"countLiversIdle{owner}") + PlayerPrefs.GetInt($"defense{owner}"))
            {
                PlayerPrefs.SetInt($"isDie{owner}", 1);
                owner = groopInTriger.owner;
                DestroyGroop(groopInTriger.thisGroop);
                bearsHear = bearsHear = Mathf.Max(groopInTriger.countBear - PlayerPrefs.GetInt($"countLiversIdle{owner}"), 1);
            }
            else if (groopInTriger.countBear * PlayerPrefs.GetInt($"force{groopInTriger.owner}") <= bearsHear * PlayerPrefs.GetInt($"force{owner}"))
            {
                bearsHear = Mathf.Max(0,bearsHear - groopInTriger.countBear* PlayerPrefs.GetInt($"force{groopInTriger.owner}"));
                DestroyGroop(groopInTriger.thisGroop);
            }
            else
            {
                if(type == 2)
                {
                    if(owner!=0) PlayerPrefs.SetInt($"countHony{owner}", PlayerPrefs.GetInt($"countHony{owner}") - 1);
                    PlayerPrefs.SetInt($"countHony{groopInTriger.owner}", PlayerPrefs.GetInt($"countHony{groopInTriger.owner}") + 1);
                }
                bearsHear = Mathf.Max(0, groopInTriger.countBear - bearsHear * PlayerPrefs.GetInt($"force{owner}"));
                owner = groopInTriger.owner;
                DestroyGroop(groopInTriger.thisGroop);
            }
        }
    }
}
public class Groop
{
    public GroopImage groopImage;
    public PlayerNavigation playerNavigation;
    public GameObject gmGroopUI;
    public GameObject gmGroopMove;
}
