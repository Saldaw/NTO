using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Camera : MonoBehaviour
{
    private GameObject Target;
    private float Distance;

    public void Start()
    {
        // getting an initial distance
        Distance = transform.position.z;

        // getting the target
        Target = GameObject.Find("PlayerCell");
    }

    public void LateUpdate()
    {
        // new camera position
        Vector3 np =
            Vector2.Lerp(
                transform.position,
                Target.transform.position,
                0.01f);

        // setting the distance
        np.z = Distance;

        // move the camera
        transform.position = np;
    }
}
