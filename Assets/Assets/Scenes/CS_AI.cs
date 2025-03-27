using System;
using Randoms = UnityEngine.Random;
using UnityEngine;
using System.Linq;

public class CS_AI : MonoBehaviour
{
    // properties
    private States State;

    // internal
    private GameObject Target;
    private Rigidbody2D rb;

    // speeds and stuff
    private float RotationSpeed;
    private float Speed;
    private float EscapeAngle = 0;

    // life-time stuff
    public float HP = 10;
    public bool IsPredator = false;
    public GameObject FoodPrefab;
    public Sprite FoodMeatySprite;

    public void Start()
    {
        // initializing values
        RotationSpeed = Randoms.value * 2 + 3;
        Speed = Randoms.value * 50 + 20;
        rb = GetComponent<Rigidbody2D>();
    }

    public void FixedUpdate()
    {
        // looking for the target
        if (Target == null && State != States.Escaping)
        {
            // chilling
            State = States.Still;

            // looking for target
            if (Randoms.value < 0.05)
                ChooseTarget();
        }

        // dead
        if (HP <= 0)
            Die();

        // state machine
        switch (State)
        {
            // non active now
            case States.Still:
                break;

            // mooving to target
            case States.GoingToFood:

                // getting the angle to the target
                float angle =
                    Mathf.Atan2(
                        Target.transform.position.y - transform.position.y,
                        Target.transform.position.x - transform.position.x);

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
                break;

            // escaping
            case States.Escaping:
                
                // im already gone wild
                if (EscapeAngle == 0)
                    EscapeAngle = Randoms.value * 2 * (float)Math.PI * Mathf.Rad2Deg;

                // rotate
                rb.AddTorque(
                    Mathf.Clamp(
                        Mathf.DeltaAngle(
                            rb.rotation,
                            EscapeAngle * Mathf.Rad2Deg - 90),
                        -RotationSpeed,
                        RotationSpeed) / 2);

                // move forward
                Vector2 velo =
                    new Vector2(
                        Speed * Time.fixedDeltaTime *
                            MathF.Cos((rb.rotation + 90) * Mathf.Deg2Rad),
                        Speed * Time.fixedDeltaTime *
                            MathF.Sin((rb.rotation + 90) * Mathf.Deg2Rad));

                // apply the force
                rb.AddForce(velo);
                break;
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        // stupid stuff
        if (other.name != "Mouth")
            return;
        HP -= other.transform.parent.gameObject.name == "PlayerCell" ? CS_Player.Damage : 4;
        Speed += 10;
        State = States.Escaping;

        // getting angle
        float angle = Mathf.Atan2(
            other.transform.position.y - transform.position.y,
            other.transform.position.x - transform.position.x) + Mathf.PI;
        
        // pushing away
        rb.AddForce(
            new Vector2(
                200 * Mathf.Cos(angle),
                200 * Mathf.Sin(angle)));
    }

    void ChooseTarget()
    {
        // selecting between foods and cells
        var list = CS_Globals.Foods;
        if (Randoms.value < 0.5 && IsPredator)
            list = CS_Globals.Cells;

        // stuff made under heavy drugs
        Target = list.Find(i => {

            // broken
            if (i == null)
                return false;

            // is the object grass
            var meaty = true;
            if (i.GetComponent<CS_Food>() != null)
                meaty = !i.GetComponent<CS_Food>().IsGrass;

            // distance to the object
            var dist =
                Vector2.Distance(
                    transform.position,
                    i.transform.position);

            // the result of search
            return dist < 15 && IsPredator == meaty;
        });

        // changing the state
        if (Target != null)
            State = States.GoingToFood;
    }

    void Die()
    {
        for (int i = 0; i < 3; i++)
        {
            // create the object
            var obj = Instantiate(FoodPrefab);

            // move the object
            obj.transform.position =
                (Vector2)transform.position +
                0.5f * Randoms.insideUnitCircle;

            // making it not grass
            obj.GetComponent<CS_Food>().IsGrass = false;
            obj.GetComponent<SpriteRenderer>().sprite = FoodMeatySprite;

            // add to the list
            CS_Generator.Objects.Add(obj);
            CS_Globals.Foods.Add(obj);
        }

        string name = GetComponent<SpriteRenderer>().sprite.name[..^1];

        if (!CS_Globals.EatenByType.ContainsKey(name))
            CS_Globals.EatenByType[name] = 0;
        CS_Globals.EatenByType[name]++;

        // dying
        Destroy(gameObject);
    }

    enum States
    {
        Still,
        GoingToFood,
        Escaping,
    }
}
