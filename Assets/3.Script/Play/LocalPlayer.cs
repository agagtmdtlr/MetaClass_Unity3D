using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class LocalPlayer : Player, IDamagable
{
    public class BaseStat
    {
        public int CurrentHp { get; set; }
    } 
    public int hp = 100;
    
    public class Events
    {
        public Action<int,int> OnDamage;
    }
    
    public Events events = new Events();
    BaseStat stat = new BaseStat();
    
    List<HitBox> hitBoxes = new List<HitBox>();
    private void Awake()
    {
        Player.localPlayer = this;
        stat.CurrentHp = hp;
    }

    private void Start()
    {
        ChangeHp(0);
        hitBoxes = GetComponentsInChildren<HitBox>(true).ToList();
        foreach (var hitbox in hitBoxes)
        {
            CombatSystem.Instance.RegisterMonster(hitbox, this);
        }
    }

    public GameObject GameObject => gameObject;
    public void TakeDamage(CombatEvent combatEvent)
    {
        Debug.Log("Hit Player");
        ChangeHp(-combatEvent.Damage);
    }

    public void ChangeHp(int amount)
    {
        stat.CurrentHp += amount;
        stat.CurrentHp = Mathf.Clamp(stat.CurrentHp, 0, hp);
        events.OnDamage?.Invoke(stat.CurrentHp, hp);

    }
}
