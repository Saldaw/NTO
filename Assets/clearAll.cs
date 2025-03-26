using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clearAll : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void ClearAll()
    {
        PlayerPrefs.DeleteAll();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
