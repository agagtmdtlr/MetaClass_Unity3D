using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Study_NaviAgent : MonoBehaviour
{
    public Transform Goal;
    public NavMeshAgent Agent;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SetDestination(Vector3 destination)
    {
        Agent.SetDestination(destination);
    }

    public void SetDestinations(List<Vector3> destinations)
    {
        StartCoroutine(MoveAgent(destinations.ToArray()));
    }

    private IEnumerator MoveAgent(Vector3[] wayPoints)
    {
        foreach (var point in wayPoints)
        {
            Agent.SetDestination(point);
            while (Agent.pathPending)
            {
                yield return null;
            }
            
            while (Agent.remainingDistance > Agent.stoppingDistance + 0.01f)
            {
                yield return null;
            }
        }
    }
}
