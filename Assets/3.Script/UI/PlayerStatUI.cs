using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatUI : StatUI
{
    [SerializeField] private LocalPlayer player;
    
    void Start()
    {
        player.events.OnDamage += UpdateHealth;
    }

}
