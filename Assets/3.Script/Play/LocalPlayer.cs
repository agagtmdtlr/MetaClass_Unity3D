using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Serialization;

public class LocalPlayer : Player, IDamagable
{
    public class Events
    {
        public Action<int,int> OnDamage;
        public Action<Weapon> OnEquipWeapon;
        
        public Action OnStartedChangeWeapon;
        public Action OnEndedChangeWeapon;
        
        public Action OnStartedReload;
        public Action OnEndedReload;
    }
    
    public readonly Events events = new Events();
    public BaseStat stat = new BaseStat();
    
    List<HitBox> hitBoxes = new List<HitBox>();
    
    public Weapon currentWeapon;
    public AnimStateEventListener animStateEventListener;
    
    [SerializeField] private Transform ItemSocket;
    
    private void Awake()
    {
        Player.localPlayer = this;
        stat.CurrentHp = stat.MaxHp;
        animStateEventListener.OnCallEvent += OnAnimEvent;
    }
    
    void OnAnimEvent(string eventName , string parameter)
    {
        if (eventName == "Reload")
        {
            currentWeapon.Reload();
            events.OnEndedReload?.Invoke();
        }
        else if( eventName == "Swap")
        {
            events.OnEndedChangeWeapon?.Invoke();
        }
    }

    private void Start()
    {
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
        stat.CurrentHp = Mathf.Clamp(stat.CurrentHp, 0, stat.MaxHp);
        events.OnDamage?.Invoke(stat.CurrentHp, stat.MaxHp);
    }
    
    void EquipWeapon(Weapon weapon)
    {
        if (currentWeapon is not null)
        {
            Debug.Log($"Destroy {currentWeapon.gameObject.name}");
            Destroy(currentWeapon.gameObject);
        }
        
        events.OnStartedChangeWeapon?.Invoke();
        currentWeapon = weapon;
        currentWeapon.gameObject.SetActive(true);
        
        currentWeapon.transform.SetParent(ItemSocket);
        currentWeapon.transform.localPosition = Vector3.zero;
        currentWeapon.transform.localRotation = Quaternion.Euler(new Vector3(90,0,0));

        events.OnEquipWeapon(currentWeapon);
    }

    public void ChangeWeapon(Weapon.WeaponType weaponType)
    {
        Debug.Log($"Change Weapon {weaponType}");
        var weapon = WeaponFactory.Instance.Create(weaponType);
        EquipWeapon(weapon);
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
