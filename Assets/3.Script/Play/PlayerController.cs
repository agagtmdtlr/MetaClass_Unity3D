using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Serialization;

public partial class PlayerController : MonoBehaviour
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

    [SerializeField] private GameObject[] statesRaw;
    
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


    private void OnEnable()
    {
        Player.localPlayer.events.OnStartedChangeWeapon += PlayChangeWeaponAnimation;
        Player.localPlayer.events.OnStartedReload += PlayRelaodAnimation;
    }

    private void OnDisable()
    {
        Player.localPlayer.events.OnStartedChangeWeapon -= PlayChangeWeaponAnimation;
    }

    void PlayChangeWeaponAnimation(Weapon weapon)
    {
        ToAttackState("Swap");
    }

    void PlayRelaodAnimation()
    {
        ToAttackState("Reload");
    }
}
