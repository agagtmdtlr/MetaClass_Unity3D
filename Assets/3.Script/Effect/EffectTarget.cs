using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.PlayerLoop;

public class EffectTarget : MonoBehaviour, IObjectPoolItem
{
    public string Key { get; set; }
    public GameObject GameObject => gameObject;

    private Vector3 direction;
    private float moveSpeed;
    
    public void Set(Vector3 direction, float speed)
    {
        if (direction == Vector3.zero)
        {
            Debug.LogWarning("EffectTarget : 방향이 설정되지 않았습니다.");
            return;
        }
        
        if (speed <= 0)
        {
            Debug.LogWarning("EffectTarget : 속도가 설정되지 않았습니다.");
            return;
        }
        this.direction = direction.normalized;
        moveSpeed = speed;
    }

    private void Update()
    {
        transform.position += direction * (moveSpeed * Time.deltaTime);
    }

    public void ReturnToPool()
    {
        ObjectPoolManager.Instance.ReturnToPool(this);
    }
}
