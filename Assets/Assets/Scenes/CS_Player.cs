using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        CS_Globals.Cells.Add(gameObject);
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        Mouth = transform.GetChild(0).gameObject;
        Time.timeScale = 1f;
    }

    public void FixedUpdate()
    {
        //Debug.Log(CS_Globals.Cells[0] + " " + CS_Globals.Cells[1] + " " + CS_Globals.Cells[2]);

        // controls ._.
        Control();

        // animate
        if (CS_Globals.Foods.Any(CheckIfNear) || CS_Globals.Cells.Where(i => i != gameObject).Any(CheckIfNear))
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

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name != "Mouth")
            return;

        // seed
        CS_Globals.Seed = (int)(CS_Globals.Seed / 1.1f);
        GetComponent<SpriteRenderer>().color = Color.black;
        SceneManager.LoadSceneAsync(7);

        // getting angle
        float angle = Mathf.Atan2(
            other.transform.position.y - transform.position.y,
            other.transform.position.x - transform.position.x) + Mathf.PI;

        // pushing away
        rb.AddForce(
            new Vector2(
                300 * Mathf.Cos(angle),
                300 * Mathf.Sin(angle)));
    }
}
