using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState : MonoBehaviour
{
    public enum StateName
    {
        Move,
        Attack
    }
    
    public abstract StateName stateName { get; }
    protected Animator animator { get; private set; }
    
    public virtual void Initialize(PlayerCharacter character)
    {
        animator = character.GetComponent<Animator>();
    }
}
