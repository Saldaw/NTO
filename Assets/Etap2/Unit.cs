using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class Unit : MonoBehaviour
{
    public State state;
    public NavMeshAgent playerNavigation;
    public int damage;
    public int dist;
    public float speed;
    private NotPlayerBuild targetB;
    public int HP;
    private bool isAttack;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < Mod.mods.Count; i++)
        {
            if (Mod.mods[i].clan != null)
            {
                playerNavigation.speed += Mod.mods[i].clan.BearSpeed;
                damage += (int)Mod.mods[i].clan.HeroDamage;
            }
        }
        animator = GetComponent<Animator>();
        state = FindObjectOfType<State>();
    }
    public void GetDamage(int damage)
    {
        HP -= damage;
        if (HP <= 0)
        {
            state.playerUnits.Remove(this);
            animator.SetBool("isDead", true);
            playerNavigation.SetDestination(transform.position);
            Destroy(this.gameObject, 1.5f);
        }
    }
    public void SetTarget(GameObject target)
    {
        animator.SetBool("isMove", true);
        targetB = target.GetComponent<NotPlayerBuild>();
        playerNavigation.SetDestination(target.transform.position);
    }
    private IEnumerator Attack()
    {
        while (targetB && Vector3.Distance(targetB.gameObject.transform.position, transform.position) < dist)
        {
            targetB.GetDamage(damage);
            yield return new WaitForSeconds(speed);
        }
        isAttack = false;
        animator.SetBool("isAttack", false);

    }
    // Update is called once per frame
    void Update()
    {
        if (!isAttack && targetB && Vector3.Distance(targetB.gameObject.transform.position, transform.position) < dist)
        {
            isAttack = true;
            animator.SetBool("isAttack", true);
            StartCoroutine(Attack());
        }
    }
}
