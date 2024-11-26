using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class PlayerNavigation : MonoBehaviour
{

    [SerializeField]private UnityEngine.AI.NavMeshAgent nav;
    public void SetTargetPosition(Vector3 pos)
    {
        nav.SetDestination(pos);
    }

}