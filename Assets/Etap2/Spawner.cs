using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private float time;
    [SerializeField] private List<GameObject> toSpawn;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnUnit());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private IEnumerator SpawnUnit()
    {
        while(true)
        {
            yield return new WaitForSeconds(time);
            Instantiate(toSpawn[Random.Range(0, toSpawn.Count)],this.transform.position,new Quaternion());
        }
        
    }
}
