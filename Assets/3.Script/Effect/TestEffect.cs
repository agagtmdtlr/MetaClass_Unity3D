using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEffect : MonoBehaviour
{
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
        var blood = ObjectPoolManager.Instance.GetObjectOrNull("Blood");
        blood.GameObject.transform.position = combatEvent.HitPosition;
        blood.GameObject.SetActive(true);
    }
}
