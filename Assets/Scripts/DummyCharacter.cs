using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class DummyCharacter : MonoBehaviour
{
    [System.Serializable]
    public class Events
    {
        public Action<int,int> OnDamage;
    }
    
    public Events Event = new Events();
    
    
    static List<Collider> s_dummyColliders = new List<Collider>();

    public static bool IsDummyCollider(Collider collider, out DummyCharacter dummy)
    {
        dummy = null;
        foreach (Collider c in s_dummyColliders)
        {
            if (c == collider)
            {
                dummy = c.GetComponent<DummyCharacter>();
                return true;
            }
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

    public void TakeDamage()
    {
        // some hp logic...
        int curhp = 0, hp = 0;
        Event.OnDamage?.Invoke(curhp,hp);
    }

    public void SpawnHitEffect(Vector3 position, Quaternion rotation)
    {
        GameObject hitEffect = Instantiate(hitEffectPrefab, position, rotation);
        StartCoroutine(UpdateHitEffect(hitEffect));
    }

}
