using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteo : MonoBehaviour
{
    public Vector3 direction;
    public Vector3 destination;
    
    public float speed = 10f;
    
    public void SetProperty(Vector3 direction, Vector3 destination)
    {
        this.direction = direction;
        this.destination = destination;
        
        transform.forward = direction;
    }

    private void Update()
    {
        if (Vector3.Dot(destination - transform.position , direction) < 0f)
        {
            var effect = ObjectPoolManager.Instance.GetObjectOrNull("Explosion");
            effect.GameObject.transform.position = transform.position;
            Destroy(gameObject);
        }
        
        transform.position += direction * (speed * Time.deltaTime);
    }
}
