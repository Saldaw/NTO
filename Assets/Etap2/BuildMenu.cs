using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class BuildMenu : MonoBehaviour
{
    [SerializeField] private State state;
    public Build build;
    public void Click(int i)
    {
        switch (i)
        {
            case 2:
                if(state.wood >= state.level*10+15 && state.honey >= state.level * 10 + 5)
                {
                    build.SetBuilding(2);
                    state.wood -= state.level*10+15;
                    state.honey -= state.level*10+5;
                    state.UpdateResurs();
                    this.gameObject.SetActive(false);
                }
                break;
            case 3:
                if (state.wood > state.level * 10 + 20)
                {
                    build.SetBuilding(3);
                    state.wood -= state.level * 10 + 20;
                    state.UpdateResurs();
                    this.gameObject.SetActive(false);
                }
                break;
            case 4:
                if (state.wood > state.level * 10 + 8)
                {
                    build.SetBuilding(4);
                    state.wood -= state.level * 10 + 8;
                    state.UpdateResurs();
                    this.gameObject.SetActive(false);
                }
                break;
        }
        
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
