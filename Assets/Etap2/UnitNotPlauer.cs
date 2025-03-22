using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class UnitNotPlauer : MonoBehaviour
{
    public State state;
    public NavMeshAgent playerNavigation;
    public int damage;
    public int dist;
    public float speed;
    public int HP;
    private Build target;
    private bool isAttack;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        state = FindObjectOfType<State>();
        List<Build> builds = state.playerBuilds;
        if(builds.Count>0)
        {
            float minDist=Vector3.Distance(this.transform.position, builds[0].transform.position);
            target = builds[0];
            for (int i = 0; i < builds.Count; i++)
            {
                if(Vector3.Distance(this.transform.position, builds[i].gameObject.transform.position)<minDist )
                {
                    minDist = Vector3.Distance(this.transform.position, builds[i].gameObject.transform.position);
                    target = builds[i];
                }
            }
            playerNavigation.SetDestination(target.gameObject.transform.position);
        }
        
    }
    private IEnumerator Attack()
    {
        while (target && Vector3.Distance(target.gameObject.transform.position, transform.position) < dist && target.type!=0)
        {
            target.GetDamage(damage);
            yield return new WaitForSeconds(speed);
        }
        isAttack = false;
        animator.SetBool("isAttack", false);
        target = null;
    }
    public void GetDamage(int damage)
    {
        HP -= damage;
        if (HP <= 0)
        {
            Destroy(this.gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            Start();
        }
        if (!isAttack && target && Vector3.Distance(target.gameObject.transform.position, transform.position) < dist)
        {
            isAttack = true;
            animator.SetBool("isAttack", true);
            StartCoroutine(Attack());
        }
    }
}
