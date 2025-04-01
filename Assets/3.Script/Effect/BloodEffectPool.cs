using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class BloodEffectPool : MonoBehaviour
{
    public GameObject bloodEffectPrefab;
    ObjectPool<Poolable> effectPool;

    private void Start()
    {
        effectPool = new ObjectPool<Poolable>(
            CreateBloodEffect,
            OnGetBloodEffect,
            OnReleaseBloodEffect,
            OnDestroyBloodEffect,
            false, 10, 100);
        
        CombatSystem.Instance.Events.OnCombatEvent += ShowBloodEffect;
    }

    public void ShowBloodEffect(CombatEvent combatEvent)
    {
        Poolable obj = effectPool.Get();
        BloodEffect effect = obj as BloodEffect;
        effect.transform.localRotation = Quaternion.identity;
        effect.transform.localPosition = Vector3.one;
        effect.transform.position = combatEvent.HitPosition;
    }    
    private void OnDisable()
    {
        effectPool.Dispose();
        CombatSystem.Instance.Events.OnCombatEvent -= ShowBloodEffect;
    }
    
    private Poolable CreateBloodEffect()
    {
        Poolable p = Instantiate(bloodEffectPrefab, Vector3.zero, Quaternion.identity).GetComponent<Poolable>();
        p.Pool = effectPool;
        return p;
    }
    private void OnGetBloodEffect(Poolable bloodEffect)
    {
        bloodEffect.gameObject.SetActive(true);
    }
    private void OnReleaseBloodEffect(Poolable bloodEffect)
    {
        bloodEffect.gameObject.SetActive(false);
    }
        
    private void OnDestroyBloodEffect(Poolable bloodEffect)
    {
        Destroy(bloodEffect.gameObject);
    }

}
