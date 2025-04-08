using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HitEffectController : MonoBehaviour
{
    void Start()
    {
        //CombatSystem.Instance.Events.OnCombatEvent += PlayBlood;
        CombatSystem.Instance.Events.OnTakeDamageEvent += PlayHitEffect;
    }

    private void OnDestroy()
    {
        //CombatSystem.Instance.Events.OnCombatEvent -= PlayBlood;
    }

    private void PlayHitEffect(TakeDamageEvent damageEvent)
    {
        var surface = damageEvent.HitBox.DamageSurface;

        string key;
        switch (surface)
        {
            case DamageSurface.Wood:
                key = "Smoke";
                break;
            case DamageSurface.Metal:
                key = "Spark";
                break;
            case DamageSurface.Skin:
                key = "Blood";
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        
        var effect = ObjectPoolManager.Instance.GetObjectOrNull(key);
        if (effect is null)
        {
            Debug.LogError(key + " couldn't find effect with type: " + surface);
        }
        
        effect.GameObject.transform.position = damageEvent.HitPosition;
        effect.GameObject.transform.forward = damageEvent.HitNormal;
        effect.GameObject.SetActive(true);
    }
}
