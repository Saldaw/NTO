using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Brevno : MonoBehaviour
{
    State state;
    // Start is called before the first frame update
    void Awake()
    {
        state = FindObjectOfType<State>();
    }
    public void OnMouseDown()
    {
        state.wood += 1;
        state.UpdateResurs();
        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
