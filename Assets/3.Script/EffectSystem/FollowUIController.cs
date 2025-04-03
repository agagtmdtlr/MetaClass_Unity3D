using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class FollowUIController : MonoBehaviour
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
        
        float duration = 2f;
        
        var target = ObjectPoolManager.Instance.GetObjectOrNull("Tweener") as Tweener;
        if(target == null)
        {
            Debug.LogError("Target is null");
        }
        target.transform.position = combatEvent.HitPosition;
        target.Set(Vector3.up, Random.Range(5f,10f),duration);
        
        
        Transform targetTransform = target.transform;
        Color color = GetDamageAreaColor(combatEvent.HitBox.DamageArea);
        
        text.Set(targetTransform, combatEvent.Damage.ToString(), duration,color );
    }

    
    
    Color GetDamageAreaColor(DamageArea area)
    {
        switch (area)
        {
            case DamageArea.Unknown:
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
