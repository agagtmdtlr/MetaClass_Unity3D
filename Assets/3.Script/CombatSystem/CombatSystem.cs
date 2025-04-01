using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatSystem : MonoBehaviour
{
    public static CombatSystem Instance { get; private set; }

    private readonly Dictionary<Collider, IDamagable> monsterDic = new Dictionary<Collider, IDamagable>();

    private void Awake()
    {
        Instance = this;
    }

    public void RegisterMonster(IDamagable monster)
    {
        if (monsterDic.TryAdd(monster.MainCollider, monster) == false)
        {
            Debug.LogWarning(
                $"{monster.GameObject.name}가 등록 되어 있습니다."
                + $"{monsterDic[monster.MainCollider].GameObject.name}를 대체합니다.");
            
            monsterDic[monster.MainCollider] = monster;
        }
    }

    public IDamagable GetMonsterOrNull(Collider collider)
    {
        return monsterDic.GetValueOrDefault(collider,null);
    }

    public void AddCombatEvent()
    {
        
    }
    

}
