using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class DummyCharacter : MonoBehaviour
{
    static List<Collider> s_dummyColliders = new List<Collider>();

    public static bool IsDummyCollider(Collider collider)
    {
        foreach (Collider c in s_dummyColliders)
        {
            if(c == collider) return true;
        }
        return false;
    }
    
    public Collider dummyCollider;
    public GameObject hitEffectPrefab;
    
    private void Start()
    {
        s_dummyColliders.Add(dummyCollider);
    }

    private IEnumerator UpdateHitEffect(GameObject hitEffect)
    {
        float lifetime =1f;

        var blood = hitEffect.GetComponent<Blood>();
        blood.InitBlood();
        
        while (lifetime > 0.0f)
        {
            blood.SetOpacity(lifetime);
            lifetime -= Time.deltaTime;
            yield return null;
        }
        Destroy(hitEffect);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(Weapon.IsWeaponCollider(other))
        {
            Vector3 hitPosition = other.ClosestPoint(transform.position);
            hitPosition = dummyCollider.ClosestPoint(other.transform.position);
            Vector3 hitNormal = (hitPosition - transform.position).normalized;

            GameObject hitEffect = Instantiate(hitEffectPrefab, hitPosition, Quaternion.LookRotation(hitNormal, Vector3.up));
            StartCoroutine(UpdateHitEffect(hitEffect));
            
            //EditorApplication.isPaused = true;

        }
    }
}
