using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.PlayerLoop;

public class Tweener : MonoBehaviour, IObjectPoolItem
{
    public string Key { get; set; }
    public GameObject GameObject => gameObject;

    private Vector3 direction;
    private float moveSpeed;
    private float duration;
    
    public void Set(Vector3 direction, float speed, float duration)
    {
        if (direction == Vector3.zero)
        {
            Debug.LogWarning("Tweener : 방향이 설정되지 않았습니다.");
            return;
        }
        
        if (speed <= 0)
        {
            Debug.LogWarning("Tweener : 속도가 설정되지 않았습니다.");
            return;
        }
        this.direction = direction.normalized;
        moveSpeed = speed;
        this.duration = duration;
    }

    private void Update()
    {
        if(duration < 0)
            ReturnToPool();
        
        duration -= Time.deltaTime;
        
        transform.position += direction * (moveSpeed * Time.deltaTime);
    }

    public void ReturnToPool()
    {
        ObjectPoolManager.Instance.ReturnToPool(this);
    }
}
