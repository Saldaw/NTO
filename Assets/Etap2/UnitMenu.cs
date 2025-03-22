using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMenu : MonoBehaviour
{
    [SerializeField] private State state;
    [SerializeField] private GameObject spawn;
    [SerializeField] private List<GameObject> prefs;
    public void Click(int i)
    {
        switch (i)
        {
            case 1:
                if (state.honey >= state.level * 5 + 5)
                {
                    var unit = Instantiate(prefs[0],spawn.transform.position,new Quaternion());
                    state.playerUnits.Add(unit.GetComponent<Unit>());
                    state.honey -= state.level * 5 + 5;
                    state.UpdateResurs();
                    this.gameObject.SetActive(false);
                }
                break;
            case 2:
                if (state.honey >= state.level * 5 + 10)
                {
                    var unit = Instantiate(prefs[1], spawn.transform.position, new Quaternion());
                    state.playerUnits.Add(unit.GetComponent<Unit>());
                    state.honey -= state.level * 5 + 10;
                    state.UpdateResurs();
                    this.gameObject.SetActive(false);
                }
                break;
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
