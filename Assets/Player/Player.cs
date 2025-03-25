using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    static readonly Color[] HP_COLORS = new Color[] { Color.gray, Color.yellow, Color.red};
    private Renderer bodyRenderer;
    
    public int hitCount = 0;
    
    public void TakeDamage(int damage)
    {
        hitCount += damage;
        hitCount = Mathf.Clamp(hitCount, 0, 3);
        if (hitCount == 3)
        {
            Destroy(gameObject);
        }
        else
        {
            bodyRenderer.material.color = HP_COLORS[hitCount % 3];
        }
    }

    private void Start()
    {
        bodyRenderer = GetComponent<Renderer>();
        TakeDamage(0);
    }

    private void OnTriggerEnter(Collider other)
    {
        BossMonster bossMonster = other.GetComponentInParent<BossMonster>();
        if (bossMonster is not null)
        {
            if (!bossMonster.WasHittedPlayer(gameObject))
            {
                bossMonster.AddHitPlayer(gameObject);
                TakeDamage(1);
            }
        }
    }
    
}
