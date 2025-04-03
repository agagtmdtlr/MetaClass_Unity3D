using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Build.Content;
using UnityEngine;

public class BossMonster : MonoBehaviour , IDamagable
{
    public GameObject GameObject => gameObject;
    
    private List<HitBox> hitBoxes;
    
    private void Start()
    {
        var hitBoxes = GetComponentsInChildren<HitBox>(true).ToList();
        foreach (var hitbox in hitBoxes)
        {
            CombatSystem.Instance.RegisterMonster(hitbox, this);
        }
    }
    
    private void OnDestroy()
    {
        foreach (var hitbox in hitBoxes)
        {
            CombatSystem.Instance.UnregisterMonster(hitbox);
        }
    }

    public void TakeDamage(CombatEvent combatEvent)
    {
        var monster = CombatSystem.Instance.GetMonsterOrNull(combatEvent.HitBox);
    }

    
    
    
    
}
