using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ParticleEffect : MonoBehaviour, IObjectPoolItem
{
    void Update()
    {
        if (particle.isPlaying == false)
        {
            ReturnToPool();
        }   
    }

    public string Key { get; set; }
    public GameObject GameObject => gameObject;
    
    public ParticleSystem particle;
    
    public void ReturnToPool()
    {
        ObjectPoolManager.Instance.ReturnToPool(this);
    }
}
