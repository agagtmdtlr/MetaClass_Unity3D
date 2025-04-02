using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class TestUIEffect : MonoBehaviour
{
    public Canvas canvas;
    public Camera uiCamera;

    void Start()
    {
        CombatSystem.Instance.Events.OnCombatEvent += PrintText;
    }

    void PrintText(CombatEvent combatEvent)
    {
        var item = ObjectPoolManager.Instance.GetObjectOrNull("FollowUI");
        if (item == null)
        {
            Debug.LogError("FollowUI is null");
        }
        
        var text = item as FollowUI;
        if(text.isInitialize == false)
            text.Initialize( uiCamera,canvas);
        
        var target = GetTarget(combatEvent);
        Color color = GetDamageAreaColor(combatEvent.Receiver.GetDamageArea(combatEvent.Collider));
        float duration = 2f;
        
        text.Set(target, combatEvent.Damage.ToString(), duration,color );
    }

    EffectTarget GetTarget(CombatEvent combatEvent)
    {
        var target = ObjectPoolManager.Instance.GetObjectOrNull("Target") as EffectTarget;
        if(target == null)
        {
            Debug.LogError("Target is null");
            return null;
        }
        target.transform.position = combatEvent.HitPosition;
        target.Set(Vector3.up, Random.Range(5f,10f));

        return target;
    }
    
    
    
    Color GetDamageAreaColor(DamageArea area)
    {
        switch (area)
        {
            case DamageArea.None:
                return Color.white;
            case DamageArea.Head:
                return Color.red;
            case DamageArea.Body:
                return Color.blue;
            case DamageArea.LeftArm:
                return Color.green;
            case DamageArea.RightArm:
                return Color.magenta;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    
}
