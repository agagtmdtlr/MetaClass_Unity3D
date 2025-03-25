using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseRayController : MonoBehaviour
{
    public Player[] players;
    public BossMonster bossMonster;
    public LayerMask whatIsHit;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if( Physics.Raycast(ray,out RaycastHit hitInfo,float.MaxValue, whatIsHit) )
            {
                var gameObject = hitInfo.collider.gameObject;
                if (gameObject.layer == LayerMask.NameToLayer("Player"))
                {
                    foreach (var player in players)
                    {
                        if(player.gameObject == gameObject)
                            player.TakeDamage(-1);
                    }    
                }
                else if (gameObject.layer == LayerMask.NameToLayer("Boss"))
                {
                    bossMonster.TakeDamage(1);
                }
            }
        }
    }
}
