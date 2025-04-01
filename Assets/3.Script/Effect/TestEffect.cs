using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEffect : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    
    void Start()
    {
        CombatSystem.Instance.Events.OnCombatEvent += PlayBlood;
    }

    private void OnDestroy()
    {
        CombatSystem.Instance.Events.OnCombatEvent -= PlayBlood;
    }


    private void PlayBlood(CombatEvent combatEvent)
    {
        Instantiate(prefab, combatEvent.HitPosition, Quaternion.identity);
    }
}
