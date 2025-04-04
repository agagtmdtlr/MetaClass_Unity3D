using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scratch : MonoBehaviour
{
    [SerializeField] int Damage = 1;
    public Collider Collider;
    
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collided with " + other.gameObject.name);
        var hitBox = other.GetComponent<HitBox>();
        if (hitBox == null)
            return;

        var target = CombatSystem.Instance.GetMonsterOrNull(hitBox);
        var hitPoint = hitBox.HitCollider.ClosestPoint(transform.position);
        var hitNormal = (hitPoint - hitBox.HitCollider.bounds.center).normalized;
        CombatEvent evt = new CombatEvent()
        {
            Damage = Damage,
            HitBox = hitBox,
            Sender = BossMonster.CurrentSceneBossMonster,
            Receiver = target,
            HitPosition = hitPoint,
            HitNormal = hitNormal
        };
        CombatSystem.Instance.AddCombatEvent(evt);
    }
}
