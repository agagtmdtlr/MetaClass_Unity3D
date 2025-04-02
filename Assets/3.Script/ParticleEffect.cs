using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEffect : MonoBehaviour, IObjectPoolItem
{
    void Update()
    {
        if (particleSystem.isPlaying == false)
        {
            ReturenToPool();
        }   
    }

    public string Key { get; set; }
    public GameObject GameObject => gameObject;
    
    public ParticleSystem particleSystem;
    
    public void ReturenToPool()
    {
        ObjectPoolManager.Instance.ReturnToPool(this);
    }
}
