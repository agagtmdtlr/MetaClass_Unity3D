using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatSystem : MonoBehaviour
{
    public const int MAX_EVENT_PROCESS_COUNT = 10;
    
    public class Callbacks
    {
        // CombatEvent가 발생하면의 의미로 씀
        public Action<CombatEvent> OnCombatEvent;
        public Action<TakeDamageEvent> OnTakeDamageEvent;
        public Action<DeathEvent> OnDeathEvent;
    }
    
    public static CombatSystem Instance { get; private set; }

    private readonly Dictionary<HitBox, IDamagable> monsterDic = new Dictionary<HitBox, IDamagable>();

    private readonly Queue<CombatEvent> eventQueue = new Queue<CombatEvent>();
    private readonly Queue<TakeDamageEvent> takeDamageQueue = new Queue<TakeDamageEvent>();
    private readonly Queue<DeathEvent> deathQueue = new Queue<DeathEvent>();
    
    public readonly Callbacks Events = new Callbacks();
    
    private void Awake()
    {
        Instance = this;
    }
    
    private void Update()
    {
        int processCount = 0;
        while (eventQueue.Count > 0 && processCount < MAX_EVENT_PROCESS_COUNT)
        {
            var combatEvent = eventQueue.Dequeue();
            combatEvent.Receiver.TakeDamage(combatEvent);
            Events.OnCombatEvent?.Invoke(combatEvent);
            processCount++;
        }

        processCount = 0;
        while (takeDamageQueue.Count > 0 && processCount < MAX_EVENT_PROCESS_COUNT)
        {
            var takeDamageEvent = takeDamageQueue.Dequeue();
            Events.OnTakeDamageEvent?.Invoke(takeDamageEvent);
            processCount++;
        }
        
        processCount = 0;
        while (deathQueue.Count > 0 && processCount < MAX_EVENT_PROCESS_COUNT)
        {
            var deathEvent = deathQueue.Dequeue();
            Events.OnDeathEvent?.Invoke(deathEvent);
            processCount++;
        }

    }
    
    public void UnregisterMonster(HitBox hitBox)
    {
        if (hitBox == null)
        {
            Debug.LogWarning("HitBox가 null입니다.");
            return;
        }
        
        if (monsterDic.Remove(hitBox) == false)
        {
            Debug.LogWarning($"{hitBox.name}가 등록 되어 있지 않습니다.");
        }
    }

    public void RegisterMonster(HitBox hitBox, IDamagable monster)
    {
        if (hitBox == null)
        {
            Debug.LogWarning($"{monster.GameObject.name}의 Collider가 null입니다.");
        }
        
        if (monsterDic.TryAdd(hitBox, monster) == false)
        {
            Debug.LogWarning(
                $"{monster.GameObject.name}가 등록 되어 있습니다."
                + $"{monsterDic[hitBox].GameObject.name}를 대체합니다.");
            
            monsterDic[hitBox] = monster;
        }
    }

    public IDamagable GetMonsterOrNull(HitBox monsterCollider)
    {
        return monsterDic.GetValueOrDefault(monsterCollider,null);
    }
    
    public void AddDeathEvent(DeathEvent deathEvent)
    {
        deathQueue.Enqueue(deathEvent);
    }

    public void AddTakeDamageEvent(TakeDamageEvent takeDamageEvent)
    {
        takeDamageQueue.Enqueue(takeDamageEvent);
    }
    
    public void AddCombatEvent(CombatEvent combatEvent)
    {
        if (IsValidEvent(combatEvent) == false)
            return;
        
        eventQueue.Enqueue(combatEvent);
    }

    bool IsValidEvent(CombatEvent combatEvent)
    {
        if (combatEvent.Sender == null)
        {
            Debug.LogWarning("Sender가 null입니다.");
            return false;
        }

        if (combatEvent.Receiver == null)
        {
            Debug.LogWarning("Receiver가 null입니다.");
            return false;
        }

        if (combatEvent.Damage <= 0)
        {
            Debug.LogWarning("Damage가 0 이하입니다.");
            return false;
        }

        return true;
    }
}
