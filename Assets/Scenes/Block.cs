using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Block : MonoBehaviour
{
    private bool[,] tipe1 = new bool[1,1] { {true} };
    private bool[,] tipe2 = new bool[2, 1] { { true }, { true } };
    private bool[,] tipe3 = new bool[1, 2] { { true, true } };
    private bool[,] tipe4 = new bool[2, 2] { { true, true } , { false, true } };
    private bool[,] tipe5 = new bool[2, 2] { { true, false }, { true, true } };
    private bool[,] tipe6 = new bool[3, 2] { { true, true }, { false, true }, { true, true } };
    private bool[,] tipe7 = new bool[3, 3] { { true, true, true }, { false, false, true }, { false, false, true } };
    [SerializeField] private int tipe;
    Image image;
    public bool isCanStand;
    public bool isStand;
    public int cell;
    const int SIZE = 20;
    public bool isSpawner = false;
    [SerializeField] CellGraidGroup graidGroup;
    [SerializeField] Transform Canvas;
    public int typeResours;

    private bool isLooking = false;
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        image.alphaHitTestMinimumThreshold = 0.1f;
    }
    public void OnPointerEnter()
    {
        if (!isSpawner && !isStand)
        {
            isLooking = true;
            graidGroup.blockNow = this;
        }
    }
    public void OnPointerClik()
    {

        if(isSpawner)
        {
            Block gm = Instantiate(this.gameObject, Canvas).GetComponent<Block>() ;
            gm.isSpawner = false;
            gm.OnPointerEnter();
        }
        if (isStand)
        {
            graidGroup.RemoveBlock(cell, GetTipe(),typeResours);
            Destroy(this.gameObject);
        }
    }
    public bool[,] GetTipe()
    {
        switch(tipe)
        {
            case 0:
                return tipe1;
            case 1:
                return tipe2;
            case 2:
                return tipe3;
            case 3:
                return tipe4;
            case 4:
                return tipe5;
            case 5:
                return tipe6;
            case 6:
                return tipe7;

            default:return null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isLooking && Input.GetMouseButton(0) && !isCanStand)
        {
            transform.position = new Vector3(Input.mousePosition.x-SIZE, Input.mousePosition.y + SIZE,0);
            image.raycastTarget = false;
            
        }
        else if (!Input.GetMouseButton(0) && isCanStand && isLooking&& isStand==false)
        {
            isStand = true;
            graidGroup.SetBlock();
            graidGroup.blockNow = null;
            image.raycastTarget = true;
        }
        else if (isLooking && !isSpawner && !Input.GetMouseButton(0) && !isStand)
        {
            graidGroup.blockNow = null;
            Destroy(this.gameObject);
        }
    }
}
