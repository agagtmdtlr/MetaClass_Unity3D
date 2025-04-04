using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Serialization;

public partial class PlayerController : MonoBehaviour , ICollector
{
    public static PlayerController CurrentPlayerController;

    public class Events
    {
        public Action<Weapon> OnEquipWeapon;
    }
    
    public readonly Events PlayerEvents = new Events();
    
    private static readonly int FIRE = Animator.StringToHash("Fire");
    private static readonly int SWAP = Animator.StringToHash("Swap");


    [Header("Animator")]
    public Animator animator;
    
    public Weapon currentWeapon;

    [Header("무기 설정")]
    [SerializeField] private Weapon[] weapons;
    [SerializeField] private GameObject[] statesRaw;

    [SerializeField] private Transform ItemSocket;
    
    private IState currentState;
    private readonly Dictionary<string,IState> attckStates = new Dictionary<string,IState>();

    [SerializeField] private Collider collectCollider;

    private void Awake()
    {
        CurrentPlayerController = this;
    }

    private void Start()
    {
        //EquipWeapon(weapons[0]);
        foreach (var stateObj in statesRaw)
        {
            var state = stateObj.GetComponent<IState>();
            attckStates[state.StateType] = state;
        }
        ToAttackState("Attack");

        ItemSystem.Instance.RegisterCollector(collectCollider, this);
    }
    
    
    public void ToAttackState(string toType)
    {
        if(currentState != null)
            currentState.Exit();
        
        currentState = attckStates[toType];
        currentState.Enter();
    }

    public bool CanSwap(int loc)
    {
        return (currentWeapon.Equals(weapons[loc]) is false);
    }

    public void SwapWeapon(int loc)
    {
        if (currentWeapon.Equals(weapons[loc]) is false)
        {
            EquipWeapon(weapons[loc]);
        }
    }

    void EquipWeapon(Weapon weapon)
    {
        if(currentWeapon != null)
            currentWeapon.gameObject.SetActive(false);
        
        currentWeapon = weapon;
        currentWeapon.gameObject.SetActive(true);
        
        currentWeapon.transform.SetParent(ItemSocket);
        currentWeapon.transform.localPosition = Vector3.zero;
        currentWeapon.transform.localRotation = Quaternion.Euler(new Vector3(90,0,0));

        animator.ResetTrigger(SWAP);
        animator.SetTrigger(SWAP);
        
        PlayerEvents.OnEquipWeapon?.Invoke(currentWeapon);
    }

    public void GetItem(ItemEvent itemEvent)
    {
        switch (itemEvent.Item)
        {
            case Item.ItemType.Unknown:
                break;
            case Item.ItemType.AssaultRifle:
                break;
            case Item.ItemType.GrenadeLauncher:
                break;
            case Item.ItemType.Shotgun:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        
        var weapon = ItemFactory.instance.Create(itemEvent.Item);
        EquipWeapon(weapon);
        
    }
}
