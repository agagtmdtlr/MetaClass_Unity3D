using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class BloodEffect : Poolable
{
    ParticleSystem particleSystem;
    void OnEnable()
    {
        particleSystem = GetComponentInChildren<ParticleSystem>(true);
    }

    private void Update()
    {
        if (!particleSystem.isPlaying)
        {
            Release();
        }
    }

}
