using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class Dynamic_Ptrol : MonoBehaviour
{
    NavMeshAgent agent;
    public float restTime = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public void SetWaypoint(Vector3[] wayPoints)
    {
        StopCoroutine(nameof(UpdatePath));
        StartCoroutine(nameof(UpdatePath), wayPoints);
    }

    IEnumerator UpdatePath(Vector3[] wayPoints)
    {
        while (gameObject.activeInHierarchy)
        {
            foreach (var p in wayPoints)
            {
                agent.SetDestination(p);

                while (agent.pathPending)
                {
                    yield return null;
                }

                while (agent.remainingDistance > agent.stoppingDistance + 0.01f)
                {
                    yield return null;
                }

                yield return new WaitForSeconds(restTime);
            }
        }
    }


}
