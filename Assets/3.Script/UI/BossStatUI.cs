using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStatUI : StatUI
{
    [SerializeField] BossMonster boss;
    // Start is called before the first frame update
    void Start()
    {
        boss.events.OnDamage += UpdateHealth;
    }

}
