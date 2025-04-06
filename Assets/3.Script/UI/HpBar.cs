using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    public static HpBar instance;
    
    public Slider BossHpBarSlider;
    public Slider PlayerHpBarSlider;

    private void Awake()
    {
        instance = this;
    }
    
    
    public Slider monsterHpBarSample;
    Dictionary<IDamagable, GameObject> monsters = new Dictionary<IDamagable, GameObject>();

    public void RegisterMonster(IDamagable damagable)
    {
        if (monsters.ContainsKey(damagable))
        {
            Debug.LogError($"Damagable already exists: {damagable.GameObject.name}");
            return;
        }
    }

    public void UnregisterMonster(IDamagable damagable)
    {
        if (monsters.ContainsKey(damagable) == false)
        {
            Debug.LogError($"Damagable does not exists: {damagable.GameObject.name}");
            return;
        }
        
        monsters.Remove(damagable);
    }

    void Start()
    {
        BossMonster.CurrentSceneBossMonster.events.OnDamage += UpdateBossHpBar;
        Player.localPlayer.events.OnDamage += UpdatePlayerHpBar;
    }

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        BossMonster.CurrentSceneBossMonster.events.OnDamage -= UpdateBossHpBar;
        Player.localPlayer.events.OnDamage -= UpdatePlayerHpBar;
    }

    private void UpdateBossHpBar(int current, int max)
    {
        BossHpBarSlider.value = (float)current / max;
    }

    private void UpdatePlayerHpBar(int current, int max)
    {
        PlayerHpBarSlider.value = (float)current / max;
    }
}
