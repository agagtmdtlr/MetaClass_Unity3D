using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class BloodEffect : Poolable
{
    ParticleSystem bloodParticle;
    void OnEnable()
    {
        bloodParticle = GetComponentInChildren<ParticleSystem>(true);
    }

    private void Update()
    {
        if (!bloodParticle.isPlaying)
        {
            Release();
        }
    }

}
