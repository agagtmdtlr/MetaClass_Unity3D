using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breath : MonoBehaviour
{
    public Collider breathCollider;
    private Transform breathPoint;
    private Transform breathDirection;
    
    List<Collider> colliders = new List<Collider>();

    public void SetProperty(Transform point, Transform worldDir)
    {
        breathPoint = point;
        breathDirection = worldDir;
    }

    public void ResetCollider()
    {
        colliders.Clear();
    }
     
    private void Update()
    {
        transform.position = breathPoint.position;
        transform.forward = breathDirection.forward;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (colliders.Contains(other)) return;
        
        CapsuleWarrior warrior = CapsuleWarrior.GetCapsuleWarrior(other);
        if (warrior != null)
        {
            colliders.Add(other);
            warrior.ChangeHp(-1);
        }
    }
}
