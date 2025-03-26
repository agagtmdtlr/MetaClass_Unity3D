using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DummyCharacter : MonoBehaviour
{
    public Collider dummyCollider;
    public GameObject hitEffectPrefab;
    List<GameObject> hitEffects = new List<GameObject>();

    private IEnumerator UpdateHitEffect(GameObject hitEffect)
    {
        float lifetime =1f;
        while (lifetime > 0.0f)
        {
            lifetime -= Time.deltaTime;
            yield return null;
        }
        Destroy(hitEffect);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter");
        if(Weapon.IsWeaponCollider(other))
        {
            Vector3 hitPosition = other.ClosestPoint(transform.position);
            hitPosition = dummyCollider.ClosestPoint(hitPosition);
            Vector3 hitNormal = (hitPosition - transform.position).normalized;

            for (int i = 0; i < 1; i++)
            {
                GameObject hitEffect = Instantiate(hitEffectPrefab, hitPosition, Quaternion.LookRotation(hitNormal, Vector3.up));
                StartCoroutine(UpdateHitEffect(hitEffect));
            }
        }
    }
}
