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
    [SerializeField] private GameObject[] statesRaw;

    [SerializeField] private Transform ItemSocket;
    
    private PlayerState currentState;
    private readonly Dictionary<string,PlayerState> attckStates = new Dictionary<string,PlayerState>();

    [SerializeField] private Collider collectCollider;

    private void Awake()
    {
        CurrentPlayerController = this;
    }

    private void Start()
    {
        foreach (var stateObj in statesRaw)
        {
            var state = stateObj.GetComponent<PlayerState>();
            state.playerController = this;
            state.playerAnimator = animator;
            attckStates[state.StateType] = state;
        }
        ToAttackState("Attack");

        ItemSystem.Instance.RegisterCollector(collectCollider, this);
    }
    
    
    public void ToAttackState(string toType)
    {
        if (currentState != null)
        {
            currentState.gameObject.SetActive(false);
            currentState.Exit();
        }

        currentState = attckStates[toType];
        currentState.gameObject.SetActive(true);
        currentState.Enter();
    }

    void EquipWeapon(Weapon weapon)
    {
        if (currentWeapon is not null)
        {
            Debug.Log($"Destroy {currentWeapon.gameObject.name}");
            Destroy(currentWeapon.gameObject);
        }
        
        currentWeapon = weapon;
        currentWeapon.gameObject.SetActive(true);
        
        currentWeapon.transform.SetParent(ItemSocket);
        currentWeapon.transform.localPosition = Vector3.zero;
        currentWeapon.transform.localRotation = Quaternion.Euler(new Vector3(90,0,0));

        ToAttackState("Swap");
        PlayerEvents.OnEquipWeapon?.Invoke(currentWeapon);
    }

    public void GetItem(ItemEvent itemEvent)
    {
        var weapon = ItemFactory.instance.Create(itemEvent.Item);
        EquipWeapon(weapon);
    }
}
