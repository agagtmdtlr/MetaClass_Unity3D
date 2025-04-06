using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ItemSpawner : MonoBehaviour
{
    public Item[] itemPrefabs;
    public Transform[] spawnTransforms;
    public float spawnRadius = 5f;

    
    public float spawnInterval = 3f;


    private void Start()
    {
        StartCoroutine(SpawnCoroutine());
    }

    private IEnumerator SpawnCoroutine()
    {
        while (gameObject.activeInHierarchy)
        {
            yield return new WaitForSeconds(spawnInterval);
            
            var selectedItem = itemPrefabs[Random.Range(0, itemPrefabs.Length)];
            var selectedTransform = spawnTransforms[Random.Range(0, spawnTransforms.Length)];
            Vector2 spawnPoint = Random.insideUnitCircle * spawnRadius;
            Vector3 spawnPosition = new Vector3(spawnPoint.x, 0, spawnPoint.y) + selectedTransform.position;
            
            var spawnItem = Instantiate(selectedItem, spawnPosition, selectedTransform.rotation);
        }
    }
    

}
