using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState : MonoBehaviour, IState
{
    public PlayerController playerController { get; set; }
    public Animator playerAnimator { get; set; }
    public abstract string StateType { get; }
    public abstract void Enter();
    public abstract void Update();
    public abstract void Exit();
    public abstract void Transition();
}
