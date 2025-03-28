using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public abstract class PlayerState : MonoBehaviour
{
    public enum StateName
    {
        Move,
        Attack1,
        Attack2,
        Attack3,
    }
    
    public abstract StateName stateName { get; }
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
