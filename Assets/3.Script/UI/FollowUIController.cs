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
        CombatSystem.Instance.Events.OnTakeDamageEvent += PrintDamageText;
    }

    void PrintDamageText(TakeDamageEvent damageEvent)
    {
        var item = ObjectPoolManager.Instance.GetObjectOrNull("FollowUI");
        if (item == null)
        {
            Debug.LogError("FollowUI is null");
        }
        
        float duration = 2f;
        
        var tweener = ObjectPoolManager.Instance.GetObjectOrNull("Tweener") as Tweener;
        if(tweener == null)
        {
            Debug.LogError("Target is null");
        }
        tweener.transform.position = damageEvent.HitPosition;
        tweener.Set(Vector3.up, Random.Range(5f,10f),duration);
        
        
        Transform targetTransform = tweener.transform;
        Color color = GetDamageAreaColor(damageEvent.HitBox.DamageArea);

        var followUI = item as FollowUI;
        if( followUI is null)
            Debug.LogWarning("followUI is null");
        followUI.Set(targetTransform, damageEvent.Damage.ToString(), duration,color );
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
