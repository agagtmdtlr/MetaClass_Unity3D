using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Serialization;

public partial class PlayerController : MonoBehaviour
{
    [Header("Animator")]
    public Animator animator;

    [SerializeField] private GameObject[] statesRaw;
    
    private PlayerState currentState;
    private readonly Dictionary<string,PlayerState> attckStates = new Dictionary<string,PlayerState>();

    [SerializeField] private Collider collectCollider;

    private void Start()
    {
        foreach (var stateObj in statesRaw)
        {
            var state = stateObj.GetComponent<PlayerState>();
            state.Context = this;
            state.PlayerAnimator = animator;
            attckStates[state.StateType] = state;
        }
        PlayAttackAnimation();
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
        if(Player.localPlayer == null)
            Debug.LogWarning($"localPlayer is null");
        
        Player.localPlayer.events.OnStartedChangeWeapon += PlayChangeWeaponAnimation;
        Player.localPlayer.events.OnEndedChangeWeapon += PlayAttackAnimation;
        
        Player.localPlayer.events.OnStartedReload += PlayRelaodAnimation;
        Player.localPlayer.events.OnEndedReload += PlayAttackAnimation;
        
    }

    private void OnDisable()
    {
        Player.localPlayer.events.OnStartedChangeWeapon -= PlayChangeWeaponAnimation;
        Player.localPlayer.events.OnEndedChangeWeapon -= PlayAttackAnimation;

        Player.localPlayer.events.OnStartedReload -= PlayRelaodAnimation;
        Player.localPlayer.events.OnEndedReload -= PlayAttackAnimation;
    }
    
    void PlayAttackAnimation()
    {
        ToAttackState("Attack");
    }

    void PlayChangeWeaponAnimation()
    {
        ToAttackState("Swap");
    }

    void PlayRelaodAnimation()
    {
        ToAttackState("Reload");
    }
}
