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
    protected PlayerCharacter character;

    public virtual void Initialize(PlayerCharacter character)
    {
        this.character = character;
        animator = character.GetComponent<Animator>();
    }
}
