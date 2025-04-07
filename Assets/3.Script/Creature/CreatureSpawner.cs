using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField] Creature[] creatures;
    [SerializeField] Transform[] spawnPoints;
    public float timeBetweenSpawns = 0.5f;

    private void OnEnable()
    {
        StartCoroutine(SpawnRoutine());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private IEnumerator SpawnRoutine()
    {
        while (gameObject.activeInHierarchy)
        {
            var selectedCreature = creatures[Random.Range(0, creatures.Length)];
            var spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];            
            
            var creature = Instantiate(selectedCreature, spawnPoint.position, Quaternion.identity);
            
            yield return new WaitForSeconds(timeBetweenSpawns);
        }
    }
    
    
    
}
