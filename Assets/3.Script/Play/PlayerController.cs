using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Serialization;

public partial class PlayerController : MonoBehaviour
{
    private static readonly int FIRE = Animator.StringToHash("Fire");
    private static readonly int SWAP = Animator.StringToHash("Swap");


    [Header("Animator")]
    public Animator animator;
    
    public Weapon currentWeapon;

    [Header("무기 설정")]
    [SerializeField] private Weapon[] weapons;
    [SerializeField] private GameObject[] statesRaw;
    
    private IState currentState;
    private Dictionary<string,IState> attckStates = new Dictionary<string,IState>();
    
    // Start is called before the first frame update
    private void Start()
    {
        EquipWeapon(weapons[0]);
        foreach (var stateObj in statesRaw)
        {
            var state = stateObj.GetComponent<IState>();
            attckStates[state.StateType] = state;
        }
        ToAttackState("Attack");
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
        currentWeapon.gameObject.SetActive(false);
        currentWeapon = weapon;
        currentWeapon.gameObject.SetActive(true);

        animator.ResetTrigger(SWAP);
        animator.SetTrigger(SWAP);
    }

}
