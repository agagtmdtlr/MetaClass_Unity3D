using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpBar : MonoBehaviour
{
    public DummyCharacter dummyCharacter;
    
    private void OnEnable()
    {
        dummyCharacter.Event.OnDamage += UpdateHpBar;
    }

    private void OnDisable()
    {
        dummyCharacter.Event.OnDamage -= UpdateHpBar;
    }
    
    void UpdateHpBar(int currentHp, int hp)
    {
        // some update logic...
    }
}
