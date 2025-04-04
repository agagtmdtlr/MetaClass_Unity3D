using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ItemEvent
{
    public ICollector Receiver { get; set; }
    public Item.ItemType Item { get; set; }
}


public class ItemSystem : MonoBehaviour
{
    public const int MAX_EVENT_PROCESS_COUNT = 10;
    
    public class Callbacks
    {
        // CombatEvent가 발생하면의 의미로 씀
        public Action<ItemEvent> OnGetItem;
    }
    
    public static ItemSystem Instance { get; private set; }

    private readonly Dictionary<Collider, ICollector> collectors = new Dictionary<Collider, ICollector>();

    private readonly Queue<ItemEvent> eventQueue = new Queue<ItemEvent>();
    
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
            var evtParam = eventQueue.Dequeue();
            evtParam.Receiver?.GetItem(evtParam);
            Events.OnGetItem?.Invoke(evtParam);
            processCount++;
        }
    }
    
    public void RegisterCollector(Collider col, ICollector collector)
    {
        if (col == null)
        {
            Debug.LogWarning($"collider가 null입니다.");
        }
        
        if (collectors.TryAdd(col, collector) == false)
        {
            Debug.LogWarning("collider가 이미 등록되어 있습니다.");
            collectors[col] = collector;
        }
    }

    public ICollector GetCollectorOrNull(Collider col)
    {
        return collectors.GetValueOrDefault(col,null);
    }
    
    public void AddWeaponEvent(ItemEvent evt)
    {
        if (evt == null)
        {
            Debug.LogWarning("Weapon이 null입니다.");
            return;
        }

        eventQueue.Enqueue(evt);
    }
    

}