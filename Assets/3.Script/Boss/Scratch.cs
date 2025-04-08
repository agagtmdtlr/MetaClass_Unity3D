using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scratch : MonoBehaviour , IAttackable
{
    public int Damage => 1;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collided with " + other.gameObject.name);
        var hitBox = other.GetComponent<HitBox>();
        if (hitBox == null)
            return;

        var target = CombatSystem.Instance.GetMonsterOrNull(hitBox);
        
        hitBox.GetHitPositionAndNormal(transform.position, out Vector3 hitPoint, out Vector3 hitNormal);
        CombatEvent evt = new CombatEvent()
        {
            HitBox = hitBox,
            Sender = this,
            Receiver = target,
            HitPosition = hitPoint,
            HitNormal = hitNormal
        };
        CombatSystem.Instance.AddCombatEvent(evt);
    }

}
