using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Pixelezator : MonoBehaviour
{
    [SerializeField] private RenderTexture texture;
    [SerializeField] private GameObject quad;
    [SerializeField] private Camera cameraMain;
    [Range(2, 16)] public float Scale = 2;

    void Start()
    {
        int width = Mathf.RoundToInt(Screen.width / Scale);
        int height = Mathf.RoundToInt(Screen.height / Scale);
        texture.width = width;
        texture.height = height;
        quad.transform.localScale = new Vector3(Screen.width*1f / Screen.height, 1, 1);
        texture.width = width;
        texture.height = height;
        quad.transform.localScale = new Vector3(Screen.width * 1f / Screen.height, 1, 1);
    }
}
