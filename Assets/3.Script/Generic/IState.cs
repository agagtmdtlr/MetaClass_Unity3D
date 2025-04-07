using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState<TKey, TContext>
{
    public TContext Context { get; }
    public TKey StateType { get; }
    public void Enter();
    public void Exit();
}


