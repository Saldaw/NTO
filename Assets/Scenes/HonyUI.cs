using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HonyUI : MonoBehaviour
{
    [SerializeField] private int numSity;

    [SerializeField] private GameObject groopCreatorUI;
    public void OnMouseDown()
    {
        groopCreatorUI.SetActive(true);
        groopCreatorUI.GetComponent<GroopCreatorUI>().UpdateInfo(numSity);
    }
    }
