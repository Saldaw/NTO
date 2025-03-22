using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Food : MonoBehaviour
{
    public int Value = 1;
    public bool IsGrass = true;

    public void OnTriggerEnter2D(Collider2D other)
    {
        // eaten only by mouth
        if (other.gameObject.name != "Mouth")
            return;

        // different script for cell and not cell
        if (other.transform.parent.name != "PlayerCell")
        {
            // destroying the object
            Destroy(gameObject);
            return;   
        }

        // is it a grass
        if (IsGrass)
            CS_Globals.GrassEaten++;
        else
            CS_Globals.MeatyEaten++;

        // doing some progress
        CS_Globals.Progress += (uint)Value;

        // destroying the object
        Destroy(gameObject);
    }
}
