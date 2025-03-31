using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class Ptrol : MonoBehaviour
{
    public Transform[] waypoints;
    NavMeshAgent agent;
    public float restTime = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        StartCoroutine(UpdatePath());
    }

    private void Update()
    {
        var path = agent.path;

        {
            for(int i = 0; i < path.corners.Length - 1; i++)
            {
                Debug.DrawLine(path.corners[i] + Vector3.up, path.corners[i + 1]+ Vector3.up, Color.green);
            }
        }
        
    }

    IEnumerator UpdatePath()
    {
        while (gameObject.activeInHierarchy)
        {
            foreach (var t in waypoints)
            {
                agent.SetDestination(t.position);

                while (agent.pathPending)
                    yield return null;

                while (agent.remainingDistance > agent.stoppingDistance + 0.01f)
                    yield return null;

                yield return new WaitForSeconds(restTime);
            }
        }

    }

}
