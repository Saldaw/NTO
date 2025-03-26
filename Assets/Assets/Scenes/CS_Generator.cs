using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CS_Generator : MonoBehaviour
{
    // radius to generate within
    const int SqRadius = 40;

    // prefabs
    public GameObject FoodPrefab;
    public GameObject BubblePrefab;
    public GameObject BubbleBGPrefab;
    public GameObject CellPrefab;

    // sprites for bg
    public List<Sprite> BubbleBGSprites;
    public List<Sprite> CellsSprites;

    // sprites for food
    public Sprite FoodGrassSprite;
    public Sprite FoodMeatySprite;

    // distribution values
    float FoodDistribution     = 1f / 100f;
    float BubbleDistribution   = 1f / 100f;
    float BubbleBGDistribution = 1f / 400f;
    float CellsDistribution    = 1f / 800f;

    // probability of meaty
    float MeatyProbability = 1f / 4f;

    // all objects generated
    static public List<GameObject> Objects =
        new List<GameObject>();

    // for cellular generation
    Vector2 PreviousPosition;

    // start
    public void Start()
    {
        // add all stuff from mods
        foreach (var m in Mod.mods)
        {
            if (m.Type != 1)
                continue;

            m.cell.Image.name += m.cell.IsPredator ? "P" : "G";

            CellsSprites.Add(m.cell.Image);
        }

        // initializing previous pos
        PreviousPosition = transform.position;

        // initial generation
        for (int x = -SqRadius; x < SqRadius; x++)
            for (int y = -SqRadius; y < SqRadius; y++)
                CreateAll(new Vector2(x, y));    
    }

    // every frame
    public void FixedUpdate()
    {
        // remove all destroyed
        Objects.RemoveAll(
            i => i == null);

        // clear other lists
        CS_Globals.Foods.RemoveAll(
            i => i == null);
        CS_Globals.Cells.RemoveAll(
            i => i == null);

        // out of bounce
        foreach (var i in Objects.ToList())
        {
            // separate distance
            float ox = Mathf.Abs(i.transform.position.x - transform.position.x);
            float oy = Mathf.Abs(i.transform.position.y - transform.position.y);

            // out of region
            if (ox > SqRadius + i.transform.position.z || oy > SqRadius + i.transform.position.z) {
                Destroy(i);
                Objects.Remove(i);
            }
        }

        // translocation x
        float dx =
            Mathf.Floor(transform.position.x) -
            Mathf.Floor(PreviousPosition.x);

        // translocation y
        float dy = 
            Mathf.Floor(transform.position.y) -
            Mathf.Floor(PreviousPosition.y);

        // create new
        for (int i = -SqRadius; i < SqRadius; i++)
        {
            // list with postion to generate in
            List<Vector2> positions =
                new List<Vector2>();

            // create on x
            if (dx != 0)
                positions.Add(
                    new Vector2(
                        transform.position.x + SqRadius * dx,
                        transform.position.y + i));
            
            // create on y
            if (dy != 0)
                positions.Add(
                    new Vector2(
                        transform.position.x + i,
                        transform.position.y + SqRadius * dy));

            // create all required object here
            foreach (var v2 in positions)
                CreateAll(v2);
        }

        // move the generation cell
        if (dx != 0 || dy != 0)
            PreviousPosition = transform.position;
    }

    // create all types
    void CreateAll(Vector2 v2)
    {
        CreateFood(v2);
        CreateBubble(v2);
        CreateBubbleBG(v2);
        CreateCell(v2);
    }

    // creating food object
    void CreateFood(Vector2 position)
    {
        // only if the chance
        if (FoodDistribution <= Random.value)
            return;

        // create the object
        var obj = Instantiate(FoodPrefab);

        // move the object
        obj.transform.position =
            position +
            Random.insideUnitCircle;

        // set random movement
        obj.GetComponent<Rigidbody2D>()
            .velocity =
                Random.insideUnitCircle *
                Random.value;

        // is meat?
        var isMeaty =
            MeatyProbability > Random.value;

        // making it grass always
        obj.GetComponent<CS_Food>()
            .IsGrass = !isMeaty;

        // painting to grass
        obj.GetComponent<SpriteRenderer>()
            .sprite = isMeaty ? FoodMeatySprite : FoodGrassSprite;

        // add to the list
        Objects.Add(obj);
        CS_Globals.Foods.Add(obj);
    }

    // creating food object
    void CreateBubble(Vector2 position)
    {
        // only if the chance
        if (BubbleDistribution <= Random.value)
            return;

        // create the object
        var obj = Instantiate(BubblePrefab);

        // move the object
        obj.transform.position =
            position +
            (Random.insideUnitCircle -
                new Vector2(0.5f, 0.5f));

        // set random movement
        obj.GetComponent<Rigidbody2D>()
            .velocity =
                Random.insideUnitCircle *
                Random.value;

        // add to the list
        Objects.Add(obj);
    }

    // create background bubbles
    void CreateBubbleBG(Vector2 position)
    {
        // only if the chance
        if (BubbleBGDistribution <= Random.value)
            return;

        // create the object
        var obj = Instantiate(BubbleBGPrefab);

        // move the object
        obj.transform.position = 
            new Vector3(
                position.x,
                position.y,
                Random.value * 6 + 5);

        // scaling random value
        float scale = Random.value * 14 + 5;

        // scaling
        obj.transform.localScale =
            new Vector2(scale, scale);

        // set random movement
        obj.GetComponent<Rigidbody2D>()
            .velocity =
                Random.insideUnitCircle *
                Random.value *
                0.5f;

        // set random sprite
        obj.GetComponent<SpriteRenderer>()
            .sprite = BubbleBGSprites[
                Random.Range(0, BubbleBGSprites.Count)];

        // add to the list
        Objects.Add(obj);
    }

    // heavy, heavy drugs...
    void CreateCell(Vector2 pos)
    {
        // only if the chance
        if (CellsDistribution <= Random.value)
            return;

        // create object
        var obj = Instantiate(CellPrefab);

        // positioning
        obj.transform.position = pos;

        // scaling
        float scale = Random.value * 2 + 4;
        obj.transform.localScale =
            new Vector2(scale, scale);

        bool isPredator = Random.value < 0.5;

        // rotating
        obj.GetComponent<Rigidbody2D>()
            .rotation = Random.value * 360;

        // randoms eating
        obj.GetComponent<CS_AI>()
            .IsPredator = isPredator;

        // get sprite
        obj.GetComponent<SpriteRenderer>()
            .sprite = RandSpriteFromList(
                CellsSprites.Where(
                    i => i.name.EndsWith(
                        isPredator ? "P" : "G")).ToList());

        // adding to lists
        Objects.Add(obj);
        CS_Globals.Cells.Add(obj);
    }

    // random item from list
    Sprite RandSpriteFromList(List<Sprite> l) =>
        l[Random.Range(0, l.Count)];
}
