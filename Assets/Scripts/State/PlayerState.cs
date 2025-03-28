using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public abstract class PlayerState : MonoBehaviour
{
    protected AnimatorStateInfo CurrentStateInfo
    {
        get { return stateAnimator.GetCurrentAnimatorStateInfo(stateLayerIndex); }
    } 
    
    public enum StateName
    {
        Move,
        ReadyAttack,
        Attack1,
        Attack2,
        Attack3,
    }
    
    public abstract StateName stateName { get; }
    
    public abstract int stateLayerIndex { get; }

    protected Animator stateAnimator { get; private set; }
    protected PlayerCharacter stateCharacter;
    
    public virtual void Initialize(PlayerCharacter character)
    {
        this.stateCharacter = character;
        stateAnimator = character.GetComponent<Animator>();
    }

    public abstract void EnterState();
    public abstract void ExitState();
}
