using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class Monster : MonoBehaviour , IDamagable
{
    public int hp = 1000;
    
    [System.Serializable]
    public class BaseStat
    {
        public int hp;
    }

    public class Events
    {
        public Action<int, int> OnDamage;
    }

    public Animator animator;
    
    public GameObject GameObject => gameObject;
    public Events events = new Events();
    BaseStat stat = new BaseStat();
    
    List<HitBox> hitboxes = new List<HitBox>();

    private void Awake()
    {
        hitboxes = GetComponentsInChildren<HitBox>(true).ToList();
        foreach (var hitbox in hitboxes)
        {
            CombatSystem.Instance.RegisterMonster(hitbox, this);
        }
        stat.hp = hp;
        
        
    }

    private void OnDestroy()
    {
        foreach (var hitbox in hitboxes)
        {
            CombatSystem.Instance.UnregisterMonster(hitbox);
        }
    }


    public void TakeDamage(CombatEvent combatEvent)
    {
        stat.hp -= combatEvent.Damage;
        stat.hp = Mathf.Clamp(stat.hp, 0, stat.hp);
        events.OnDamage?.Invoke(stat.hp, stat.hp);
    }
}
