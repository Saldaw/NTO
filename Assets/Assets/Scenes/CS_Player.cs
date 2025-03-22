using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CS_Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sr;

    public static float Speed = 70;
    public static float RotationSpeed = 5;
    public static float Damage = 4;

    public Sprite IdleSprite;
    public Sprite AttackSprite;
    public Sprite MoveSprite;

    private GameObject Mouth;

    // near the food or cell
    bool CheckIfNear(GameObject i) =>
        i != null &&
        Vector2.Distance(
            i.transform.position,
            Mouth.transform.position) < 4;

    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        Mouth = transform.GetChild(0).gameObject;
        Time.timeScale = 1f;
    }

    public void Update()
    {
        // controls ._.
        Control();

        // animate
        if (CS_Globals.Foods.Any(CheckIfNear) || CS_Globals.Cells.Any(CheckIfNear))
            sr.sprite = AttackSprite;
        else if (Time.time * 1000 % 500 < 250 && Input.GetMouseButton(0))
            sr.sprite = MoveSprite;
        else
            sr.sprite = IdleSprite; 
    }

    private void Control()
    {
        // no upgrade
        if (CS_Upgrade.Show)
            return;

        // if left mouse button in pressed
        if (!Input.GetMouseButton(0))
            return;

        // mouse position
        Vector3 mp = Input.mousePosition;

        // plane to get position on
        mp.z = transform.position.z -
               Camera.main.transform.position.z;

        // world position
        Vector3 wp =
            Camera.main.ScreenToWorldPoint(mp);

        // relative to player position
        Vector2 rp = wp - transform.position;

        // too small distance
        if (rp.magnitude < 0.3)
            return;

        // getting the angle to the position
        float angle = Mathf.Atan2(rp.y, rp.x);

        // rotate
        rb.AddTorque(
            Mathf.Clamp(
                Mathf.DeltaAngle(
                    rb.rotation,
                    angle * Mathf.Rad2Deg - 90),
                -RotationSpeed,
                RotationSpeed) / 2);

        // move forward
        Vector2 vel =
            new Vector2(
                Speed * Time.fixedDeltaTime *
                    MathF.Cos((rb.rotation + 90) * Mathf.Deg2Rad),
                Speed * Time.fixedDeltaTime *
                    MathF.Sin((rb.rotation + 90) * Mathf.Deg2Rad));

        // apply the force
        rb.AddForce(vel);
    }
}
