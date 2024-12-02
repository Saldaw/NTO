using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CellGraidGroup : MonoBehaviour
{
    [SerializeField] private GameObject cell;
    [SerializeField] private int startScale;
    [SerializeField] private PlayerFabric playerFabric;
    private List<Cell> cells = new List<Cell>(18);
    public Block blockNow;
    private int cellNow;
    private int scaleNow;
    private bool[,] filling = new bool[6, 3] { {false, false, false}, { false, false, false }, { false, false, false }, { true, true, true }, { true, true, true }, { true, true, true } };
    private int g = 1;
    // Start is called before the first frame update
    void Start()
    {
        scaleNow = startScale;
        for (int i = 0; i<startScale*3; i++)
        {
            GameObject gm = Instantiate(cell, this.transform);
            gm.GetComponent<Cell>().SetInfo(g, this);
            cells.Add(gm.GetComponent<Cell>());
            g++;
        }

    }
    public void SetBlock()
    {
        blockNow.cell = cellNow;
        int x0 = (cellNow) / 3;
        int y0 = (cellNow) % 3;
        bool[,] blockSize = blockNow.GetTipe();
        for (int x = 0; x < blockSize.GetLength(0); x++)
        {
            for (int y = 0; y < blockSize.GetLength(1); y++)
            {
                if (blockSize[x,y]) filling[x0 + x, y0 + y] = true;
            }
        }
        switch (blockNow.typeResours)
        {
            case 0:
                playerFabric.materials += 1;
                break;
            case 1:
                playerFabric.electronics += 1;
                break;

            case 2:
                playerFabric.weapons += 1;
                break;
        }
    }
    public void RemoveBlock(int cell, bool[,] blockSizeParametr, int resType)
    {
        int x0 = (cell) / 3;
        int y0 = (cell) % 3;
        for (int x = 0; x < blockSizeParametr.GetLength(0); x++)
        {
            for (int y = 0; y < blockSizeParametr.GetLength(1); y++)
            {
                if (blockSizeParametr[x, y]) filling[x0 + x, y0 + y] = false;
            }
        }
        switch (resType)
        {
            case 0:
                playerFabric.materials -= 1;
                break;
            case 1:
                playerFabric.electronics -= 1;
                break;

            case 2:
                playerFabric.weapons -= 1;
                break;
        }
    }
    public void CanFill(int cell)
    {
        if (blockNow && blockNow.isStand==false)
        {
            int x0 = (cell - 1) / 3;
            int y0 = (cell - 1) % 3;
            bool[,] blockSize = blockNow.GetTipe();
            for (int x =0;x< blockSize.GetLength(0); x++)
            {
                for (int y = 0; y < blockSize.GetLength(1); y++)
                {
                    if (blockSize.GetLength(0) - 1 + x0 <= scaleNow && blockSize.GetLength(1) - 1 + y0 <= 2)
                    {
                        if (filling[x0 + x, y0 + y] == true && blockSize[x, y])
                        {
                            return;
                        }
                    }
                    else return;
                }
            }
            cellNow = cell - 1;
            blockNow.isCanStand = true;
            blockNow.transform.position = cells[cell-1].transform.position;
        }
    }
    public void AddLean(int i)
    {
        for (int j = 0; j<i; j++)
        {
            filling[j + scaleNow, 0] = false;
            filling[j + scaleNow, 1] = false;
            filling[j + scaleNow, 2] = false;
            GameObject gm = Instantiate(cell, this.transform);
            gm.GetComponent<Cell>().SetInfo(g, this);
            cells.Add(gm.GetComponent<Cell>());
            g++;
            gm = Instantiate(cell, this.transform);
            gm.GetComponent<Cell>().SetInfo(g, this);
            cells.Add(gm.GetComponent<Cell>());
            g++;
            gm = Instantiate(cell, this.transform);
            gm.GetComponent<Cell>().SetInfo(g, this);
            cells.Add(gm.GetComponent<Cell>());
            g++;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
