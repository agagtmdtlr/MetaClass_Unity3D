using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState : MonoBehaviour, IState<string, PlayerController>
{
    public PlayerController Context { get; set; }
    public abstract string StateType { get; }
    public Animator PlayerAnimator { get; set; }
    public abstract void Enter();
    public abstract void Update();
    public abstract void Exit();
}
