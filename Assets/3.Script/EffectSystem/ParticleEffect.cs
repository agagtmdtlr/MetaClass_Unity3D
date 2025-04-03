using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(ParticleSystem))]
public class ParticleEffect : MonoBehaviour, IObjectPoolItem
{
    protected virtual void Update()
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
