using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Globalable<GameManager>
{
    public int GameScore { get; private set; }
    
    void Start()
    {
        CombatSystem.Instance.Events.OnDeathEvent += OnDeathEvent;
    }

    void OnDeathEvent(DeathEvent deathEvent)
    {
        if (deathEvent.Dead.DamageableType == typeof(Creature))
        {
            GameScore++;
        }
    }
}
