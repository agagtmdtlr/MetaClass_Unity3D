using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Serialization;

public class LocalPlayer : Player, IDamagable, ICollector
{
    public class BaseStat
    {
        public int CurrentHp { get; set; }
    } 
    public int hp = 100;
    
    public class Events
    {
        public Action<int,int> OnDamage;
        public Action<Weapon> OnChangedWeapon;
        public Action<Weapon> OnStartedChangeWeapon;
        public Action<Weapon> OnEndedChangeWeapon;
        
        public Action OnStartedReload;
        public Action OnEndedReload;
        
    }
    
    public Events events = new Events();
    BaseStat stat = new BaseStat();
    
    List<HitBox> hitBoxes = new List<HitBox>();
    
    public Weapon currentWeapon;
    public AnimStateEventListener animStateEventListener;
    
    [SerializeField] private Transform ItemSocket;
    
    private void Awake()
    {
        Player.localPlayer = this;
        stat.CurrentHp = hp;

        animStateEventListener.OnCallEvent += OnReloaded;
    }
    
    void OnReloaded(string eventName , string parameter)
    {
        if (eventName == "Reload")
        {
            events.OnStartedReload?.Invoke();
            currentWeapon.Reload();
            events.OnEndedReload?.Invoke();
        }
    }

    private void Start()
    {
        HpBar.instance.RegisterPlayer(this);
        ChangeHp(0);
        hitBoxes = GetComponentsInChildren<HitBox>(true).ToList();
        foreach (var hitbox in hitBoxes)
        {
            CombatSystem.Instance.RegisterMonster(hitbox, this);
        }
        CombatSystem.Instance.Events.OnDeathEvent += OnDeathEvent;
    }
    
    private void OnDeathEvent(DeathEvent obj)
    {
        ChangeHp(5);
    }

    public Collider MainCollider => collider;
    [SerializeField] private Collider collider;
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
    
    void EquipWeapon(Weapon weapon)
    {
        if (currentWeapon is not null)
        {
            Debug.Log($"Destroy {currentWeapon.gameObject.name}");
            Destroy(currentWeapon.gameObject);
        }
        
        
        events.OnStartedChangeWeapon?.Invoke(currentWeapon);
        currentWeapon = weapon;
        currentWeapon.gameObject.SetActive(true);
        
        currentWeapon.transform.SetParent(ItemSocket);
        currentWeapon.transform.localPosition = Vector3.zero;
        currentWeapon.transform.localRotation = Quaternion.Euler(new Vector3(90,0,0));

        events.OnEndedChangeWeapon?.Invoke(currentWeapon);
    }

    public void OnRequiredItem(ItemEvent itemEvent)
    {
        var weapon = WeaponFactory.instance.Create(itemEvent.Item);
        EquipWeapon(weapon);
    }

    public void ChangeWeapon(Weapon.WeaponType weaponType)
    {
        Debug.Log($"Change Weapon {weaponType}");
    }

    public void OnTouchedItem(Item.ItemType itemType)
    {
        if (currentWeapon is null)
        {
            ChangeWeapon(Mapper.MapWeapon(itemType));
        }
        else if (currentWeapon.Type != Mapper.MapWeapon(itemType))
        {
            ChangeWeapon(Mapper.MapWeapon(itemType));
        }
    }
}
