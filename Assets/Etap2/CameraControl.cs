using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    private Vector2 previousMousePosition;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        previousMousePosition = Input.mousePosition;
        if (previousMousePosition.x / Screen.width > 0.9f) MoveCamera(1, 0);
        if (previousMousePosition.x / Screen.width < 0.1f) MoveCamera(-1, 0);
        if (previousMousePosition.y / Screen.height > 0.9f) MoveCamera(0, 1);
        if (previousMousePosition.y / Screen.height < 0.1f) MoveCamera(0, -1);
    }
    void MoveCamera(int x, int y)
    {

    }
}
