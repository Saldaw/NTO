using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotPlayerBuild : MonoBehaviour
{
    [SerializeField] private State state;
    [SerializeField] private Image progBar;
    [SerializeField] private int type = 0;
    [SerializeField] private int MaxHP;
    [SerializeField] private int HP;
    public GameObject decal;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void GetDamage(int damage)
    {
        HP -= damage;
        progBar.fillAmount = 1f * HP /MaxHP;
        if (HP <= 0)
        {
            if ( type == 1 ) 
            {
                state.Win();
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
    }
    public void OnMouseDown()
    {
        state.SetTargetBuild(this);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
