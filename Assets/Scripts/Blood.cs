using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using Random = UnityEngine.Random;

public class Blood : MonoBehaviour
{
    private static readonly int OPACITY = Shader.PropertyToID("_Opacity");
    private static readonly int BASE_MAP = Shader.PropertyToID("Base_Map");
    public DecalProjector decal;

    public Material[] materials;

    public void InitBlood()
    {
        int materialIndex = Random.Range(0, materials.Length);
        var material =  materials[materialIndex];
        decal.material = material;
        
    }

    public void SetOpacity(float opacity)
    {
        decal.material.SetFloat(OPACITY, opacity);
    }

}
