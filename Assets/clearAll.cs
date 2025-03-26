using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clearAll : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        CS_Globals.Seed = (int)(Random.value*10000);
    }
    public void ClearAll()
    {
        PlayerPrefs.DeleteAll();
    }
    public void SetSeed(string seed)
    {
        CS_Globals.Seed =int.Parse(seed);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
