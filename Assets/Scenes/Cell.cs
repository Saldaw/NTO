using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Cell : MonoBehaviour
{
    CellGraidGroup graidGroup;
    int num;
    public void OnPointerEnter()
    {
        graidGroup.CanFill(num);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void SetInfo(int i, CellGraidGroup cellGraidGroup)
    {
        num = i;
        graidGroup = cellGraidGroup;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
