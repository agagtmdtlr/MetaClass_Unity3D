using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    public Slider BossHpBarSlider;
    public Slider PlayerHpBarSlider;
    // Start is called before the first frame update
    void Start()
    {
        BossMonster.CurrentSceneBossMonster.Event.OnDamage += UpdateBossHpBar;
        Player.localPlayer.events.OnDamage += UpdatePlayerHpBar;
    }

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        BossMonster.CurrentSceneBossMonster.Event.OnDamage -= UpdateBossHpBar;
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
