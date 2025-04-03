using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
    public string StateType { get; }
    public void Enter();
    public void Update();
    public void Exit();
    
    public void Transition();
}


