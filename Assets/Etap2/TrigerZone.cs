using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class TrigerZone : MonoBehaviour
{
    public bool isPlayer;
    bool isAttack;
    List<GameObject> listToAttack = new List<GameObject>();
    [SerializeField] private int damage;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
        if(!isPlayer && other.tag== "PlayerUnits")
        {
            listToAttack.Add(other.gameObject);
        }
        if (isPlayer && other.tag == "NotPlayerUnit")
        {
            listToAttack.Add(other.gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (!isPlayer && other.tag == "PlayerUnits")
        {
            listToAttack.Remove(other.gameObject);
        }
        if (isPlayer && other.tag == "NotPlayerUnit")
        {
            listToAttack.Remove(other.gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (listToAttack.Count > 0 && !isAttack)
        {
            isAttack = true;
            StartCoroutine(Attack());
        }
    }
    private IEnumerator Attack()
    {
        yield return new WaitForSeconds(0.1f);
        if (listToAttack.Count > 0)
        {
            if (listToAttack[0] == null)
            {
                listToAttack.RemoveAt(0);
            }
            if (isPlayer)
            {
                if (listToAttack.Count > 0)
                {
                    listToAttack[0].GetComponent<UnitNotPlauer>().GetDamage(damage);
                }
            }
            else
            {
                if (listToAttack.Count > 0)
                {
                    listToAttack[0].GetComponent<Unit>().GetDamage(damage);
                }
            }
            isAttack = false;
        }
        else
        {
            isAttack = false;
        }
        
    }
}
