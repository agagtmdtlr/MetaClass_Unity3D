using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteoSpawner : MonoBehaviour
{
    [SerializeField] private Meteo[] meteoSamples;
    [SerializeField] private float spawnInterval = 0.1f;
    
    [SerializeField] private Transform[] spawnTransform;
    [SerializeField] private Transform[] destionations;

    public float speed;

    private void Start()
    {
        StartCoroutine(SpawnMeteo());
    }

    private IEnumerator SpawnMeteo()
    {
        while (gameObject.activeInHierarchy)
        {
            var sample = meteoSamples[Random.Range(0, meteoSamples.Length)];
            var spawnPoint = spawnTransform[Random.Range(0, spawnTransform.Length)];
            var destination = destionations[Random.Range(0, destionations.Length)];
            
            var meteo = Instantiate(sample, spawnPoint.position, Quaternion.identity);
            meteo.SetProperty((destination.position - spawnPoint.position).normalized, destination.position);
            meteo.speed = speed;
            yield return new WaitForSeconds(spawnInterval);
        }
    }
    
    
    
}
