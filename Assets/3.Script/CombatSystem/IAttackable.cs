using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttackable
{
    public void BeginAttack();
    
    public void AddHitted(IDamagable target);
    public bool IsHitted(IDamagable damagable);
}
