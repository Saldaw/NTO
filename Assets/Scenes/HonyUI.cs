using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HonyUI : MonoBehaviour
{
    [SerializeField] private int numSity;
    private GroopController groopController;
    [SerializeField] private GameObject TreeUI;
    private void Start()
    {
        groopController = GetComponent<GroopController>();
    }
    public void OnMouseUp()
    {
        TreeUI.SetActive(true);
        TreeUI.GetComponent<TreeUI>().UpdateInfo(groopController);
    }
    }
