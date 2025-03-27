using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnOffDecal : MonoBehaviour
{
    [SerializeField] GameObject decal;
    void OnMouseEnter()
    {
        decal.SetActive(true);
    }

    // ...the red fades out to cyan as the mouse is held over...
    void OnMouseExit()
    {
        decal.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
