using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Bubble : MonoBehaviour
{
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.otherRigidbody.velocity.magnitude > 0.1)
            Destroy(gameObject);
    }
}
