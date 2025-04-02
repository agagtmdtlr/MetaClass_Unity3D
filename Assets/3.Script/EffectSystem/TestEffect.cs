using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEffect : MonoBehaviour
{
    void Start()
    {
        //CombatSystem.Instance.Events.OnCombatEvent += PlayBlood;
        CombatSystem.Instance.Events.OnCombatEvent += PlayHitEffect;
    }

    private void OnDestroy()
    {
        //CombatSystem.Instance.Events.OnCombatEvent -= PlayBlood;
    }

    private void PlayHitEffect(CombatEvent combatEvent)
    {
        var surface = combatEvent.Receiver.GetDamageSurface(combatEvent.Collider);

        string key;
        switch (surface)
        {
            case DamageSurface.Wood:
                key = "Smoke";
                break;
            case DamageSurface.Metal:
                key = "Spark";
                break;
            case DamageSurface.Orginic:
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
        
        effect.GameObject.transform.position = combatEvent.HitPosition;
        effect.GameObject.transform.forward = combatEvent.HitNormal;
        effect.GameObject.SetActive(true);
    }

    private void PlayBlood(CombatEvent combatEvent)
    {
        var blood = ObjectPoolManager.Instance.GetObjectOrNull("Blood");
        
        
        blood.GameObject.transform.position = combatEvent.HitPosition;
        blood.GameObject.SetActive(true);
    }
}
