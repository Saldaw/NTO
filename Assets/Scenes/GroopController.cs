using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class GroopController : MonoBehaviour
{
    [SerializeField] private Sprite sprite;
    [SerializeField] private GameObject groopMove;
    [SerializeField] private GameObject groopUI;
    [SerializeField] private Tiker tiker;
    [SerializeField] private int point;
    [SerializeField] private int owner;
    public int bearsHear;
    [SerializeField] int type;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public Groop CreateGroop(int count, Vector3 pos, int targetPoint)
    {
        GameObject gmGroopUI = Instantiate(groopUI,transform.position, Quaternion.Euler(90,0,0));
        GameObject gmGroopMove = Instantiate(groopMove, transform.position, Quaternion.Euler(0, 0, 0));
        Groop groop = new Groop() {groopImage = gmGroopUI.GetComponent<GroopImage>(),playerNavigation = gmGroopMove.GetComponent<PlayerNavigation>(), gmGroopUI = gmGroopUI, gmGroopMove = gmGroopMove};
        groop.groopImage.targetPosition = gmGroopMove;
        groop.groopImage.StartPosition = transform.position;
        groop.groopImage.targetWalking = targetPoint;
        groop.groopImage.startWalking = point;
        groop.groopImage.owner = owner;
        groop.groopImage.SetInfo(sprite, count, groop,this);
        groop.playerNavigation.SetTargetPosition(pos);
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
        if (Input.GetMouseButtonDown(1) && owner ==5) CreateGroop(10, new Vector3(1500,6,1500),1);
    }
    private void OnTriggerEnter(Collider other)
    {
        GroopImage groopInTriger = other.GetComponent<GroopImage>();
        if (groopInTriger && groopInTriger.targetWalking == point && owner==groopInTriger.owner) { bearsHear += groopInTriger.countBear; DestroyGroop(groopInTriger.thisGroop); }
        else if (groopInTriger && groopInTriger.targetWalking == point) 
        {
            if (groopInTriger.countBear * PlayerPrefs.GetInt($"force{groopInTriger.owner}") <= bearsHear * PlayerPrefs.GetInt($"force{owner}"))
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
